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
    public class FileWebConfigController : ControllerBase
    {
        private readonly IFileWebConfigService _fileWebConfigService;

        public FileWebConfigController(IFileWebConfigService fileWebConfigService)
        {
            this._fileWebConfigService = fileWebConfigService;

        }
        [HttpGet]
        public async Task<WebResponseContent> GetFileWebConfig()
        {
            return await _fileWebConfigService.GetFileWebConfig();
        }
    }
}