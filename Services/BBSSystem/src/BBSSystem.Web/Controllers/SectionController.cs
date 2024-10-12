using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.SectionApp;
using BBSSystem.Application.Contracts.SectionApp.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BBSSystem.Web.Controllers
{
    [ApiController]
    [Route("bbs/[controller]")]
    public class SectionController : ControllerBase
    {
        public ISectionService SectionService { get; set; }

        [HttpGet]
        public async Task<SectionSimpleDto> GetSectionSimpleDtoAsync(string sectionId)
        {
            return await SectionService.GetSectionSimpleDtoAsync(sectionId);
        }
    }
}