using UploadSystem.Ability.Docking;
using Microsoft.Extensions.DependencyInjection;
using Youshow.Ace.Http.Client;
using Youshow.Ace.Modularity;

namespace UploadSystem.Http.Client;
[RelyOn(
        typeof(AceHttpClientModule)
)]
public class UploadSystemHttpClientModule : AceModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAceHttpClient(opt =>
        {
           opt.AddRemoteModule<UploadSystemAbilityDockingModule>("Default");
        });
    }

}
