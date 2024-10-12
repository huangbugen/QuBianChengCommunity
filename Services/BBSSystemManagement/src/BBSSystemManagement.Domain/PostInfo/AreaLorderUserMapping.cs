using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youshow.Ace.Domain.Models;

namespace BBSSystemManagement.Domain.PostInfo
{
    public class AreaLorderUserMapping : BaseModel<string>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AreaId { get; set; }

        public AreaLorderUserMapping(string id) : base(id)
        {

        }
    }
}