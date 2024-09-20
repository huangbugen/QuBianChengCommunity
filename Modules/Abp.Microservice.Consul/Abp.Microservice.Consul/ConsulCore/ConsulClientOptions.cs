using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Microservice.Consul.ConsulCore
{
    public class ConsulClientOptions
    {
        public string Ip { get; set; }
        public int Port { get; set; }
    }
}