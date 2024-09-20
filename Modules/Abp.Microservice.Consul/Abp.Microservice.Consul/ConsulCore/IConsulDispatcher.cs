using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Microservice.Consul.ConsulCore
{
    public interface IConsulDispatcher
    {
        string GetUrl(string mappingUrl);
        string GetFullUrl(string groupName, string apiKey);
    }
}