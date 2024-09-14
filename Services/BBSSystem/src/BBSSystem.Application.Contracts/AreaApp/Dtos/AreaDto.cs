using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.SectionApp.Dtos;
using Volo.Abp.Application.Dtos;

namespace BBSSystem.Application.Contracts.AreaApp.Dtos
{
    public class AreaDto : EntityDto<string>
    {
        public string AreaName { get; set; }
        public int Sort { get; set; }
        public List<SectionDto> Sections { get; set; }
    }
}