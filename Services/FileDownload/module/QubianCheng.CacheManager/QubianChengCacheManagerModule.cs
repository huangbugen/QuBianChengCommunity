
using Autofac;
using FileDownLoadSystem.Core.Configuration;
using Microsoft.Extensions.Caching.Memory;
using QubianCheng.CacheManager.IService;

namespace QubianCheng.CacheManager;
public class QubianChengCacheManagerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MemoryCache>().As<IMemoryCache>().SingleInstance();
        if (AppSetting.UseRedis)
        {
            builder.RegisterType<RedisCacheService>().As<ICacheService>().SingleInstance();
        }
        else
        {
            builder.RegisterType<MemoryCacheService>().As<ICacheService>().SingleInstance();
        }
    }
}
