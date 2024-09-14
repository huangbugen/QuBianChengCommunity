using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBSSystem.Application.Contracts.SectionApp.Dtos
{
    public class SectionLordUserMappingDto : EntityDto<string>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SectionId { get; set; }
    }
}