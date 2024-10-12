using Microsoft.Extensions.DependencyInjection;
using UploadSystem.Domain;
using Youshow.Ace.EntityFrameworkCore;
using Youshow.Ace.Modularity;

namespace UploadSystem.EntityFrameworkCore;
[RelyOn(
    typeof(AceEntityFrameworkCoreModule),
    typeof(UploadSystemDomainModule)
)]
public class UploadSystemEntityFrameworkCoreModule : AceModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAceDbContext<UploadSystemDbContext>(opt=>
        {
            opt.AddDefaultRepositories(true);
        });
        Configure<AceDbContextOptions>(opt=>{
            opt.UseMySQL();
        });
    }
}
