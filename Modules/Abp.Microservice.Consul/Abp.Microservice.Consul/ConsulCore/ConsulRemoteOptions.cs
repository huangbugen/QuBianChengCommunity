using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Microservice.Consul.ConsulCore
{
    public class ConsulRemoteOptions
    {
        public List<ConsulRemoteGroupItem> GroupItems { get; set; }
    }

    public class ConsulRemoteGroupItem
    {
        public string GroupName { get; set; }
        public List<ConsulRemoteApiAddrItem> Api { get; set; }
    }

    public class ConsulRemoteApiAddrItem
    {
        public string ApiKey { get; set; }
        public string ApiAddr { get; set; }
    }
}