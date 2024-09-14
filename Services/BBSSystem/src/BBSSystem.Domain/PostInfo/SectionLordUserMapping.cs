using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace BBSSystem.Domain.PostInfo
{
    public class SectionLordUserMapping : Entity<string>
    {
        public SectionLordUserMapping(string id) : base(id)
        {

        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SectionId { get; set; }
    }
}