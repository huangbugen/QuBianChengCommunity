using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Abp.AspNet.JwtBearer
{
    public class AbpAspNetJwtBearerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }
    }
}