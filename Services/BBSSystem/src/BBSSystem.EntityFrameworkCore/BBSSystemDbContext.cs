using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.PostInfo;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BBSSystem.EntityFrameworkCore
{
    public class BBSSystemDbContext : AbpDbContext<BBSSystemDbContext>
    {
        public DbSet<Area> Area { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<SectionLordUserMapping> SectionLordUserMapping { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostType> PostType { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public BBSSystemDbContext(DbContextOptions<BBSSystemDbContext> options) : base(options)
        {
        }
    }
}