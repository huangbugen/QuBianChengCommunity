using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace UserSystem.Domain.Account
{
    public class Role : Entity<long>
    {
        public string RoleName { get; set; }
        public string ServiceGroup { get; set; }
    }
}