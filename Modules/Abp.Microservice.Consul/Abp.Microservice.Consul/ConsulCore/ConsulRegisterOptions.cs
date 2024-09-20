using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Microservice.Consul.ConsulCore
{
    public class ConsulRegisterOptions
    {
        public string ServiceGroup { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string HealthUrl { get; set; }
        public string HttpScheme { get; set; }
    }
}