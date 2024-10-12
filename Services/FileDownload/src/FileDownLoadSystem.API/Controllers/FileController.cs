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
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            this._fileService = fileService;
            
        }
        
        [HttpGet]
        public async Task<WebResponseContent> GetFilesByTypeId(int fileTypeId)
        {
            return await _fileService.GetFilesByTypeId(fileTypeId);
        }
        [HttpGet("singal")]
        public async Task<WebResponseContent> GetFilesById(int fileId)
        {
            return await _fileService.GetFilesById(fileId);
        }
    }
}