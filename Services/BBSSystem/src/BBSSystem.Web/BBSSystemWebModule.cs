using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNet.JwtBearer;
using BBSSystem.Application;
using BBSSystem.Domain.Shared.Claims;
using BBSSystem.HttpApi.Client;
using BBSSystem.Web.Filters;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BBSSystem.Web
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(BBSSystemApplicationModule),
        typeof(BBSSystemHttpAPIClientModule),
        typeof(AbpAspNetJwtBearerModule)
    )]
    public class BBSSystemWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddEndpointsApiExplorer();
            context.Services.AddScoped<ICurrentClaims, CurrentClaims>();
            context.Services.AddControllers(c => c.Filters.Add<CurrentUserAuthorizationFilterAttribute>());

            var configuration = context.Services.GetConfiguration();
            var origins = configuration.GetSection("AllowOrigins").Get<string[]>();
            context.Services.AddCors(c => c.AddDefaultPolicy(cc => cc.AllowAnyHeader().AllowAnyMethod().WithOrigins(origins!).AllowCredentials()));

            context.Services.AddHttpClient();

            context.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "xxxxxxxxxxxxxx",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                          Reference = new OpenApiReference{
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                          }
                      },
                      new string[]{}
                    }

                });
            });
            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            if (context.GetEnvironment().IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseConfiguredEndpoints();
            base.OnApplicationInitialization(context);
        }
    }
}