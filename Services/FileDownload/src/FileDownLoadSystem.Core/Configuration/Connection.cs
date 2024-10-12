using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Core.Configuration
{
    public class Connection
    {
      public string DBType { get; set; }  
      public string DbConnectionString { get; set; }
      public string[] RedisConnectionString { get; set; }
      public bool UseRedis { get; set; }
    }
}