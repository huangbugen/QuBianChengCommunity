using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Volo.Abp;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace UserSystem.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpCachingStackExchangeRedisModule)
    )]
    public class UserSystemDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RedisCacheOptions>(options =>
            {

            });
            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }
    }
}