using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Microservice.Consul.ConsulCore;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.Microservice.Consul.Extensions
{
    public static class ConsulWebApplicationExtensions
    {
        public static void UseConsul(this IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var consulClient = serviceProvider.GetRequiredService<IConsulClient>();

            RegisterConsul(configuration, consulClient);
        }

        public static void RegisterConsul(IConfiguration configuration, IConsulClient consulClient)
        {
            var consulRegisterOptions = new ConsulRegisterOptions();
            configuration.Bind("Consul:ConsulRegister", consulRegisterOptions);

            // 士兵的类型
            string consulGroup = consulRegisterOptions.ServiceGroup;

            // 士兵的名字
            int port = consulRegisterOptions.Port;
            string ip = consulRegisterOptions.Ip;
            string serviceName = $"{consulGroup}_{ip}_{port}";

            var check = new AgentServiceCheck
            {
                Interval = TimeSpan.FromSeconds(6),
                // 健康检查地址
                HTTP = $"{consulRegisterOptions.HttpScheme}://{ip}:{port}{consulRegisterOptions.HealthUrl}",
                // 请求后2秒内未响应，则认为服务以停止
                Timeout = TimeSpan.FromSeconds(2),
                // 服务停止后2秒后注销服务
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(2)
            };

            var register = new AgentServiceRegistration
            {
                Check = check,
                Address = ip,
                ID = serviceName,
                Name = consulGroup,
                Port = port
            };

            consulClient.Agent.ServiceRegister(register);
        }

        private static void CheckHealth(WebApplication webApplication)
        {
            webApplication.MapGet("/heart", () =>
            {
                return new OkResult();
            });
        }
    }
}