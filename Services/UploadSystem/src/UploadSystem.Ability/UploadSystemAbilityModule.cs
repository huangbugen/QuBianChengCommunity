using UploadSystem.EntityFrameworkCore;
using UploadSystem.Ability.Docking;
using Youshow.Ace.Ability;
using Youshow.Ace.AutoMapper;
using Youshow.Ace.Modularity;
using Youshow.Ace.Logger;
using Microsoft.Extensions.DependencyInjection;
using Youshow.Ace.File.Upload;

namespace UploadSystem.Ability;
[RelyOn(
    typeof(AceAbilityModule),
    typeof(AceAutoMapperModule),
    typeof(AceLoggerModule),
    typeof(AceFileUploadModule),
    typeof(UploadSystemAbilityDockingModule),
    typeof(UploadSystemEntityFrameworkCoreModule)
)]
public class UploadSystemAbilityModule : AceModule
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
