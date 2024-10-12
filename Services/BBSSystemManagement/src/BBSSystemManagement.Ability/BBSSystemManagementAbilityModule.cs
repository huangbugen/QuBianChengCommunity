using BBSSystemManagement.EntityFrameworkCore;
using BBSSystemManagement.Ability.Docking;
using Youshow.Ace.Ability;
using Youshow.Ace.AutoMapper;
using Youshow.Ace.Modularity;
using Youshow.Ace.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace BBSSystemManagement.Ability;
[RelyOn(
    typeof(AceAbilityModule),
    typeof(AceAutoMapperModule),
    typeof(AceLoggerModule),
    typeof(BBSSystemManagementAbilityDockingModule),
    typeof(BBSSystemManagementEntityFrameworkCoreModule)
)]
public class BBSSystemManagementAbilityModule : AceModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        services.AddAceLogger(
            opt=>opt.UseFile()
                    .UseElasticSearch(false)
        );
    }
}
