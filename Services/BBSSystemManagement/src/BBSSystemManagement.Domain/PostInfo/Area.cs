using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Youshow.Ace.Domain.Models;

namespace BBSSystemManagement.Domain.PostInfo
{
    public class Area : BaseModel<string>
    {
        public string AreaName { get; set; }
        public int Sort { get; set; }
        public string IsDeleted { get; set; }

        [NotMapped]
        public List<AreaLorderUserMapping> AreaPadLorders { get; set; }

        public Area(string id) : base(id)
        {

        }
    }
}