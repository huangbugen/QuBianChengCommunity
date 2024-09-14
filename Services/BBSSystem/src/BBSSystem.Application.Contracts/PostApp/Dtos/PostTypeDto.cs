using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBSSystem.Application.Contracts.PostApp.Dtos
{
    public class PostTypeDto : EntityDto<string>
    {
        public string PostTypeName { get; set; }
        public string SectionId { get; set; }
        public int Order { get; set; }
    }
}