using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UserSystem.Application.Contracts.UserApp;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace BBSSystem.HttpApi.Client
{
    [DependsOn(
        typeof(AbpHttpClientModule)
    )]
    public class BBSSystemHttpAPIClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxy(typeof(IUserService));
            base.ConfigureServices(context);
        }
    }
}