using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.SectionApp;
using BBSSystem.Application.Contracts.SectionApp.Dtos;
using BBSSystem.Domain.Managers;
using BBSSystem.Domain.PostInfo;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.SectionApp
{
    public class SectionService : ApplicationService, ISectionService
    {
        public SectionManager SectionManager { get; set; }

        public async Task<SectionSimpleDto> GetSectionSimpleDtoAsync(string sectionId)
        {
            var res = await SectionManager.GetSimpleSectionsAsync(sectionId);
            var section = res.section;
            var lordNames = res.lordNames;
            var dto = ObjectMapper.Map<Section, SectionSimpleDto>(section);
            dto.SectionLorders = lordNames;
            return dto;
        }
    }
}