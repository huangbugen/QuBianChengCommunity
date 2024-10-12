using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BBSSystemManagement.Ability.Docking.LocalSersvices.SensiticeApp;
using BBSSystemManagement.Domain.Managers;
using BBSSystemManagement.Domain.Shared.Cto;
using BBSSystemManagement.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Youshow.Ace.Ability;
using Youshow.Ace.Cache.Redis;
using Youshow.Ace.File.Upload;
using Youshow.Ace.File.Upload.Extensions;
using static BBSSystemManagement.Domain.PostInfo.SensitiveWordsLibrary;

namespace BBSSystemManagement.Ability.SensitiveApp
{
    public class SensitiveService : AbilityServicer, ISensitiveService
    {
        public IRedisCache Cache { get; set; }
        public BBSSystemManagementDbContext Context { get; set; }
        public IFileUploadManager FileUploadManager { get; set; }
        public SensitiveWordManager SensitiveWordManager { get; set; }

        public async Task SaveFile(IFormFile file)
        {
            var uploadContext = FileUploadManager
                .AddSaveFolder("/FileUpload/SensitiveWords")
                .AddFileExtensions(".txt")
                .ResetFileExtensions()
                .BuildUploadContext(file);

            await uploadContext.FileUploadAsync()
            .Success(async fileInfo =>
            {
                await SensitiveWordManager.AddSensitiveWordsLibrary(fileInfo);
            })
            .Exists(fileInfo =>
            {
                throw new Exception("敏感词库重复添加");
            })
            .Error(ex =>
            {
                throw ex;
            });
        }

        public async Task SetLibraryStatusAsync(string libraryId, SensitiveStatus status)
        {
            // 方案一：整体获取，修改完数据后在整体更新。由于更新了不需要更新的字段，所以不太好
            // var res = await SensitiveWordManager.SensitiveWordsLibraryRepo.GetAsync(libraryId);
            // res.Status = status;
            // await SensitiveWordManager.SensitiveWordsLibraryRepo.UpdateAsync(res);

            // 一下都是指定更新列，用任意一个都可以
            // // 一、Attach，这种方案由于单独引用了上下文，所以脱离了工作单元
            // var model = new SensitiveWordsLibrary(libraryId);
            // model.Status = status;
            // Context.Attach(model);
            // Context.Entry(model).Property("Status").IsModified = true;
            // Context.SaveChanges();

            // // 二、Sql语句，这种方式的缺点是在代码中使用了SQL，让我们的代码可维护性变差，应该单独封装在底层较好，同样这种方案由于单独引用了上下文，所以脱离了工作单元
            // Context.FromSqlRaw("UPDATE SensitiveWordsLibrary SET Status=@Status WHERE Id=@Id",
            //     new MySqlParameter("@Id",libraryId),
            //     new MySqlParameter("@Status",status));
            // Context.SaveChanges();    

            // // 三、EFCore7.0开始的批量更新，推荐使用，但是这种方案由于单独引用了上下文，所以脱离了工作单元
            // Context.Set<SensitiveWordsLibrary>().Where(m=>m.Id == libraryId)
            //     .ExecuteUpdate(m=>m.SetProperty(p=>p.Status,status));
            // Context.SaveChanges();

            // // 四、AceFramework封装的方法，基于框架，推荐使用，可以灵活配置需要更新的内容
            // await SensitiveWordManager.SensitiveWordsLibraryRepo
            //     .UpdateAsync(m=>m.Id == libraryId, new {Status = status});

            // // 五、AceFramework封装的方法，基于框架，推荐使用，更新列的选择及其灵活，可以通过前端传入需要修改的列名
            // await SensitiveWordManager.SensitiveWordsLibraryRepo
            //     .UpdateAsync(m => m.Id == libraryId, new Dictionary<string, object> {{ "Status" , status} });

            // // 六、AceFramework封装的方法，基于框架，推荐使用，优势在于可以自动点出列名
            // await SensitiveWordManager.SensitiveWordsLibraryRepo
            //     .UpdateAsync(m => m.Id == libraryId, p => new Dictionary<object, object> { { p.Status, status } });
            await SensitiveWordManager.UpdateStatusAsync(libraryId, status);
        }

        public async Task SetWordsInCacheAsync()
        {
            List<string> lineList = new();
            WebClient webClient = new WebClient();
            //从文件中获取敏感词
            var sensitive = await SensitiveWordManager.GetSensitiveWordsLibrariesInActiveAsync();
            sensitive.ForEach(m =>
            {
                byte[] buffer = webClient.DownloadData(m.LibraryFileUrl);
                var text = Encoding.GetEncoding("UTF-8").GetString(buffer);
                var array = text.Split("\r\n");
                foreach (var item in array)
                {
                    var itemArray = item.Split("，");
                    lineList.AddRange(itemArray);
                }
            });
            lineList = lineList.Distinct().ToList();
            lineList.Remove("");

            SensitiveWordCto cto = new(lineList);
            Cache.SetHashMemory(cto, TimeSpan.FromDays(30));
        }

        public async Task<List<string>> GetWordsInCacheAsync()
        {
            /*
            既然要从缓存中获取，必然涉及到了一个叫做缓存击穿的问题
            如何防止缓存击穿的发生，我们要用到一个叫做双if，中间夹锁的代码结构
            */
            // 先获取内存中的值
            var cto = Cache.GetHashMemory<SensitiveWordCto>();
            if (cto == null)
            {
                var lockKey = "lock";
                // 分布式缓存双if用于极端情况，这个和本地lock的逻辑是不同的
                if (Cache.Lock(lockKey, TimeSpan.FromSeconds(10)))
                {
                    cto = Cache.GetHashMemory<SensitiveWordCto>();
                    if (cto == null)
                    {
                        await SetWordsInCacheAsync();
                        cto = Cache.GetHashMemory<SensitiveWordCto>();
                        Cache.UnLock(lockKey);
                    }
                    else
                    {
                        Cache.UnLock(lockKey);
                    }
                }

            }
            var words = cto.Words;
            var wordList = JsonSerializer.Deserialize<List<string>>(words);
            return wordList;
        }
    }
}