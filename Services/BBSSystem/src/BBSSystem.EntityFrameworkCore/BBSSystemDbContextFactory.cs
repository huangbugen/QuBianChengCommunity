using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BBSSystem.EntityFrameworkCore
{
    public class BBSSystemDbContextFactory : IDesignTimeDbContextFactory<BBSSystemDbContext>
    {
        public BBSSystemDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var builder = new DbContextOptionsBuilder<BBSSystemDbContext>()
                                .UseMySql(configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);
            return new BBSSystemDbContext(builder.Options);
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BBSSystem.Web"))
                                .AddJsonFile("appsettings.json", false);
            return builder.Build();
        }
    }
}