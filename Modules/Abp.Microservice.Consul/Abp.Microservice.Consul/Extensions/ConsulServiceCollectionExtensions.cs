using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Microservice.Consul.ConsulCore;
using Consul;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.Microservice.Consul.Extensions
{
    public static class ConsulServiceCollectionExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            services.AddSingleton<IConsulClient>(c => new ConsulClient(cc =>
            {
                cc.Address = new Uri("http://1.94.42.172:8500");
            }));
            return services;
        }

        public static IServiceCollection AddConsulDispatcher(this IServiceCollection services)
        {
            services.AddTransient<IConsulDispatcher, ConsulDispatcher>();
            return services;
        }
    }
}