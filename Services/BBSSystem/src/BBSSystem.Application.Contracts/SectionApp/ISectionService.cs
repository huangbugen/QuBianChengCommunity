using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.SectionApp.Dtos;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.Contracts.SectionApp
{
    public interface ISectionService : IApplicationService
    {
        Task<SectionSimpleDto> GetSectionSimpleDtoAsync(string sectionId);
    }
}