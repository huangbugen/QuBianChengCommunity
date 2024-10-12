using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Untity;
using FileDownLoadSystem.System.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FileDownLoadSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileTypeController : ControllerBase
    {
        private readonly IFileTypeService _fileTypeService;
        public FileTypeController(IFileTypeService fileTypeService)
        {
            this._fileTypeService = fileTypeService;

        }
        [HttpGet]
        public async Task<WebResponseContent> GetFileTypes()
        {
            return await _fileTypeService.GetFileTypes();
        }
    }
}