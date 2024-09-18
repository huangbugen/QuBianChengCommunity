using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNet.JwtBearer;
using UserSystem.Application;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace UserSystem.Web
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(UserSystemApplicationModule),
        typeof(AbpAspNetJwtBearerModule)
    )]
    public class UserSystemWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddControllers();
            context.Services.AddEndpointsApiExplorer();
            context.Services.AddSwaggerGen();
            context.Services.AddControllers();

            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            base.OnApplicationInitialization(context);
        }
    }
}