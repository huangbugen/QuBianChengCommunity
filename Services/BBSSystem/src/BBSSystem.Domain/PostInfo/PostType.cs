using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace BBSSystem.Domain.PostInfo
{
    public class PostType : Entity<string>
    {
        public PostType(string id) : base(id)
        {

        }

        public string PostTypeName { get; set; }
        public string SectionId { get; set; }
        public int Order { get; set; }
    }
}