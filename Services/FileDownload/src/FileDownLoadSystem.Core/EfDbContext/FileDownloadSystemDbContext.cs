using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Extensions;
using FileDownLoadSystem.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace FileDownLoadSystem.Core.EfDbContext
{
    public class FileDownloadSystemDbContext : DbContext , IDependency
    {
        public FileDownloadSystemDbContext()
        {

        }
        public FileDownloadSystemDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
               optionsBuilder.UseMySql("Server=192.168.31.201;database=QbcFileDownloadSystem;uid=root;pwd=123456",MySqlServerVersion.LatestSupportedServerVersion); 
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                // 获取所有的内裤
                // DependencyContext依赖Microsoft.Extensions.DependencyModel包
                var compilationLibrary = DependencyContext.Default.CompileLibraries
                .Where(x=> !x.Serviceable && x.Type != "package" && x.Type=="project");
                
                // 获取所有数据库模型
                foreach (var _compilation in compilationLibrary)
                {
                    System.Console.WriteLine(_compilation.Name);
                    // 加载指定类型
                    AssemblyLoadContext.Default
                    .LoadFromAssemblyName(new AssemblyName(_compilation.Name))
                    .GetTypes()
                    .Where(x=>
                    x.GetTypeInfo().BaseType != null
                    && !x.IsAbstract
                    && x.BaseType == (typeof(BaseModel)))
                    .ToList().ForEach(t=>{
                        modelBuilder.Entity(t);
                    });
                }
                 base.OnModelCreating(modelBuilder);
            }
            catch (System.Exception)
            {
            }
           
        }

    }
}