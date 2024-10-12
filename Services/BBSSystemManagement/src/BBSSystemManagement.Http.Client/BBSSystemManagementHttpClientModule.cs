using BBSSystemManagement.Ability.Docking;
using Microsoft.Extensions.DependencyInjection;
using Youshow.Ace.Http.Client;
using Youshow.Ace.Modularity;

namespace BBSSystemManagement.Http.Client;
[RelyOn(
        typeof(AceHttpClientModule)
)]
public class BBSSystemManagementHttpClientModule : AceModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAceHttpClient(opt =>
        {
           opt.AddRemoteModule<BBSSystemManagementAbilityDockingModule>("Default");
        });
    }

}
