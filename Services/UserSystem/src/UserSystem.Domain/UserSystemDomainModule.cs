using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace UserSystem.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule)
    )]
    public class UserSystemDomainModule : AbpModule
    {

    }
}