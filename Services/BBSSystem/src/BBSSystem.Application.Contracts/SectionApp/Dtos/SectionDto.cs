using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBSSystem.Application.Contracts.SectionApp.Dtos
{
    public class SectionDto : EntityDto<string>
    {
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string SectionTitle { get; set; }
        public string SectionIcon { get; set; }
        public string SectionDescription { get; set; }
        public string SectionPlaybill { get; set; }
        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }
        public long PostAllCount { get; set; }
        public long PostTodayCount { get; set; }
        public List<SectionLordUserMappingDto> SectionLords { get; set; }
    }
}