using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Youshow.Ace.Data;
using Youshow.Ace.EntityFrameworkCore;

namespace BBSSystemManagement.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class BBSSystemManagementDbContext : AceDbContext<BBSSystemManagementDbContext>
    {
        public BBSSystemManagementDbContext(DbContextOptions<BBSSystemManagementDbContext> options) : base(options)
        {
        }
    }
}
