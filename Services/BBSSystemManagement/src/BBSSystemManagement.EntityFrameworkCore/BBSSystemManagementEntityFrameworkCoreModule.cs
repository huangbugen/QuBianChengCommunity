using Microsoft.Extensions.DependencyInjection;
using BBSSystemManagement.Domain;
using Youshow.Ace.EntityFrameworkCore;
using Youshow.Ace.Modularity;

namespace BBSSystemManagement.EntityFrameworkCore;
[RelyOn(
    typeof(AceEntityFrameworkCoreModule),
    typeof(BBSSystemManagementDomainModule)
)]
public class BBSSystemManagementEntityFrameworkCoreModule : AceModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAceDbContext<BBSSystemManagementDbContext>(opts =>
        {
            opts.AddDefaultRepositories(true);
        });
        Configure<AceDbContextOptions>(opts =>
        {
            opts.UseMySQL();
        });
    }
}
