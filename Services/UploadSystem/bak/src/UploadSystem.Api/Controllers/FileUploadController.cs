using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploadSystem.Api.Utility;

namespace UploadSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileUploadController : ControllerBase
    {
        public FileUploadController(IUploadFileHelper uploadFileHelper)
        {
            UploadFileHelper = uploadFileHelper;
        }

        public IUploadFileHelper UploadFileHelper { get; }

        [HttpPost]
        public string UploadFileIcon(IFormFile file)
        {
            var path = @"E:\网站发布\UploadFile\QbcFileIcon\";
            var rootPath = @"http://localhost:8098/QbcFileIcon/";
            var fileext = ".gif|.jpg|.jpeg|.png|.webp";
            return UploadFileHelper.SaveFile(file,path,rootPath,fileext);
        }
    }
}