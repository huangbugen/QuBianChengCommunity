using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNet.JwtBearer;
using Microsoft.OpenApi.Models;
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

            var configuration = context.Services.GetConfiguration();
            var origins = configuration.GetSection("AllowOrigins").Get<string[]>();
            context.Services.AddCors(c => c.AddDefaultPolicy(cc => cc.AllowAnyHeader().AllowAnyMethod().WithOrigins(origins!).AllowCredentials()));

            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserSystem", Version = "v1" });
                c.DocInclusionPredicate((docName, description) => true);
            });

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(UserSystemApplicationModule).Assembly);
            });

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

            app.UseCors();

            app.UseHttpsRedirection();

            base.OnApplicationInitialization(context);
        }
    }
}