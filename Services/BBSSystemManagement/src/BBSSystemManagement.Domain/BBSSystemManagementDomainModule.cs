using Youshow.Ace.Cache.Redis;
using Youshow.Ace.Domain;
using Youshow.Ace.File.Upload;
using Youshow.Ace.MicroService.Consul;
using Youshow.Ace.Modularity;

namespace BBSSystemManagement.Domain;
[RelyOn(
    typeof(AceDomainModule),
    typeof(AceFileUploadModule),
    typeof(AceCacheRedisModule),
    typeof(AceMicroServiceConsulModule)
)]
public class BBSSystemManagementDomainModule : AceModule
{

}
