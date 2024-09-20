using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;

namespace Abp.Microservice.Consul.ConsulCore
{
    public class ConsulDispatcher : IConsulDispatcher
    {
        private readonly IConsulClient _consulClient;
        private readonly IConfiguration _configuration;

        public ConsulDispatcher(
            IConsulClient consulClient,
            IConfiguration configuration
        )
        {
            this._consulClient = consulClient;
            this._configuration = configuration;
            ConsulClientOptions consulClientOptions = new();
            configuration.Bind("Consul:ConsulClient", consulClientOptions);
        }

        public string GetUrl(string mappingUrl)
        {
            var uri = new Uri(mappingUrl);
            var serviceName = uri.Host;
            var scheme = uri.Scheme;
            var ipAndPoint = AnalysisConsulService(serviceName);
            return $"{scheme}://{ipAndPoint}{uri.PathAndQuery}";
        }

        public string GetFullUrl(string groupName, string apiKey)
        {
            var options = new ConsulRemoteOptions();
            _configuration.Bind("ConsulRemote", options);
            var addr = options.GroupItems.FirstOrDefault(m => m.GroupName.Equals(groupName))?.Api.FirstOrDefault(m => m.ApiKey.Equals(apiKey))?.ApiAddr;
            var baseUrl = GetUrl($"http://{groupName}");
            var fullUrl = baseUrl + addr;
            return fullUrl;
        }

        public string AnalysisConsulService(string serviceName)
        {
            // 获取所有注册在Consul代理上的服务
            var response = _consulClient.Agent.Services().Result.Response;
            // 过滤出serviceName服务，忽略大小写
            var keyValuePairs = response.Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).ToArray();
            // 负载均衡
            var index = new Random().Next(0, keyValuePairs.Length);
            var serviceInfo = keyValuePairs[index].Value;

            var url = $"{serviceInfo.Address}:{serviceInfo.Port}";
            return url;
        }
    }
}