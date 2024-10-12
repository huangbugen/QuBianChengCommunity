using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Youshow.Ace.File.Upload;
using Youshow.Ace.File.Upload.Extensions;

namespace UploadSystem.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileUploadController : ControllerBase
    {
        public IFileUploadManager FileManager { get; set; }
        [HttpPost]
        public async Task UploadFileIcon(IFormFile file)
        {
            //创建文件上传上下文
            var uploadContext = FileManager
                   .AddSaveFolder("QbcFileIcon") // 文件具体存放的目录，如果不写则默认存放根目录
                   .AddMaxLength(1024 * 1024)  // 文件上传大小限制（会覆盖配置文件得SizeLimit），不得大于服务器允许上传大小 即：MaxRequestBodySize
                   .AddFileExtensions(".gif", ".jpg", ".jpeg", ".png", ".webp")
                   .ResetFileExtensions(true)
                   .BuildUploadContext();      // 创建上下文

            // 上传文件并返回回调函数
            await uploadContext.FileUploadAsync(file)
            .Success(fileInfo =>
            {
                // 添加成功时回调业务逻辑
            })
            .Exists(fileInfo =>
            {
                // 文件已有，无需重复添加时回调业务逻辑
            })
            .Error(ex =>
            {
                throw ex; //出错时返回
            });
        }
    }
}