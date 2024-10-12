using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadSystem.Ability;
using UploadSystem.Http.Client;
using Microsoft.OpenApi.Models;
using Youshow.Ace;
using Youshow.Ace.AspNetCore.Web;
using Youshow.Ace.AspNetCore.Web.Conventions;
using Youshow.Ace.Modularity;

namespace UploadSystem.Web
{
    [RelyOn(
        typeof(AceAspNetCoreWebModule),
        typeof(UploadSystemHttpClientModule),
        typeof(UploadSystemAbilityModule)
    )]
    public class UploadSystemWebModule : AceModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Add services to the container.
            context.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            context.Services.AddEndpointsApiExplorer();
            context.Services.AddCors(c=>c.AddDefaultPolicy(p=>p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("UploadSystem", new OpenApiInfo { Title = "UploadSystem.Web", Version = "v1" });
                //再swagger中显示Application中暴露的接口
                c.DocInclusionPredicate((docName, description) => true);
                //添加安全定义
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传递)直接在下面框中输入Bearer {token}(注意两者之间是一个空格)",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //添加安全要求
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                //参考类型为SecurityScheme
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        }, new string[] { }
                    }
                });
            });
            Configure<AceAspNetCoreWebOptions>(opt =>
            {
                opt.Create<UploadSystemAbilityModule>();
            });
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetWebApplication();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/UploadSystem/swagger.json", "UploadSystem.Web v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
