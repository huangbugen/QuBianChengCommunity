using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.AreaApp;
using BBSSystem.Application.Contracts.AreaApp.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BBSSystem.Web.Controllers
{
    [ApiController]
    [Route("bbs/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            this._areaService = areaService;
        }

        [HttpGet]
        public async Task<List<AreaDto>> GetAreaDtosAsync(int pageIndex = 1, int pageSize = 30)
        {
            var res = await _areaService.GetAreaDtoAsync(pageIndex, pageSize);
            return res;
        }
    }
}