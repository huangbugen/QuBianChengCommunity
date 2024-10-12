using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Autofac;
using FileDownLoadSystem.Core.Configuration;
using FileDownLoadSystem.Core.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace FileDownLoadSystem.Core.Extensions
{
    public static class AutofacServiceCollectionExtensions
    {
        public static IServiceCollection AddModule(this IServiceCollection services, ContainerBuilder builder, IConfiguration configuration)
        {
            AppSetting.Init(services, configuration);

            // 拿到与本项目有关联的项目
            var compileLibraries = DependencyContext.Default.CompileLibraries
            .Where(x => !x.Serviceable && x.Type == "project").ToList();
            // 获取程序集
            List<Assembly> assemblies = new();
            foreach (var compliation in compileLibraries)
            {
                assemblies.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(compliation.Name)));
            }
            // 找实现IDependency的累，然后注册
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(type => !type.IsAbstract && type.IsAssignableTo<IDependency>())
                .AsSelf().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserContext>().AsSelf();




            return services;
        }
    }
}