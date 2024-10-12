using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Microservice.Consul;
using BBSSystem.Application.Contracts;
using BBSSystem.EntityFrameworkCore;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace BBSSystem.Application
{
    [DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreSignalRModule),
    typeof(BBSSystemEntityFrameworkCoreModule),
    typeof(AbpMicroserviceConsulModule)
    )]
    public class BBSSystemApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(opt =>
            {
                opt.AddProfile<BBSSystemProfile>();
            });
            base.ConfigureServices(context);
        }
    }
}