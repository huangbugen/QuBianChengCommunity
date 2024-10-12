using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace UploadSystem.Api.Utility
{
    public class UploadFileHelper : IUploadFileHelper
    {
        public UploadFileHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string SaveFile(IFormFile file, string savePath, string rootPath, string fileext)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            // 文件扩展名
            var fileExt = Path.GetExtension(file.FileName);
            if (file == null)
            {
                return "";
            }
            if (fileext != null && fileext.IndexOf(fileExt.ToLower()) <= -1)
            {
                return "";
            }
            // 判断文件大小
            long length = file.Length;
            if (length > Convert.ToInt64(Configuration["MaxFileSize"]))
            {
                return "";
            }

            // 存文件的hash名
            var hash = SHA1.Create();
            var hashBytes = hash.ComputeHash(file.OpenReadStream());
            var saveName = BitConverter.ToString(hashBytes).Replace("-", "") + fileExt;
            FileInfo fileInfo = new(savePath + saveName);
            if (!fileInfo.Exists)
            {
                using FileStream fs = File.Create(savePath + saveName);
                file.CopyTo(fs);
                fs.Flush();
            }
            savePath = $"{rootPath}{saveName}";
            return savePath;
        }
    }
}