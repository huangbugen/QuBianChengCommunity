using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Youshow.Ace.Data;
using Youshow.Ace.EntityFrameworkCore;

namespace UploadSystem.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class UploadSystemDbContext : AceDbContext<UploadSystemDbContext>
    {
        public UploadSystemDbContext(DbContextOptions<UploadSystemDbContext> options) : base(options)
        {
        }
    }
}
