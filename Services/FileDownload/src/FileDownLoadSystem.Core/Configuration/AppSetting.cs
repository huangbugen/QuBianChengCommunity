using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Consts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileDownLoadSystem.Core.Configuration
{
    public class AppSetting
    {
        public static Dictionary<string, string> Connections { get; set; }
        // 获取Connection对象
        private static Connection _connection;

        public static bool UseRedis => _connection.UseRedis;
        public static string[] RedisConnectionString => _connection.RedisConnectionString;

        public static Secret Secret { get; private set; }
        public static int ExpMinutes { get; private set; } = 1;
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            //Microsoft.Extensions.Configuration.Binder
            _connection = configuration.GetSection("Connection").Get<Connection>();
            Secret = configuration.GetSection("Secret").Get<Secret>();
        }
    }
}