using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.AreaApp;
using BBSSystem.Application.Contracts.AreaApp.Dtos;
using BBSSystem.Application.Contracts.SectionApp.Dtos;
using BBSSystem.Domain.Managers;
using BBSSystem.Domain.PostInfo;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.AreaApp
{
    public class AreaService : ApplicationService, IAreaService
    {
        public AreaManager AreaManager { get; set; }
        public SectionManager SectionManager { get; set; }

        public async Task<List<AreaDto>> GetAreaDtoAsync(int pageIndex, int pageSize)
        {
            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;

            var areas = await AreaManager.GetAreasAsync(skip, take);
            var areaDtos = ObjectMapper.Map<List<Area>, List<AreaDto>>(areas);

            var sections = await SectionManager.GetSectionsByAreaIdAsync(areaDtos.Select(m => m.Id));
            var sectionDtos = ObjectMapper.Map<List<Section>, List<SectionDto>>(sections);

            areaDtos.ForEach(m =>
            {
                m.Sections = sectionDtos.FindAll(n => n.AreaId == m.Id);
            });

            return areaDtos;
        }
    }
}