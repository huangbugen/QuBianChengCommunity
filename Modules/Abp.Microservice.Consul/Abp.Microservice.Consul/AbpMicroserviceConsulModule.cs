using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Microservice.Consul.Extensions;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Abp.Microservice.Consul
{
    public class AbpMicroserviceConsulModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddConsul().AddConsulDispatcher();
            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseConsul(context.ServiceProvider);
            base.OnApplicationInitialization(context);
        }
    }
}