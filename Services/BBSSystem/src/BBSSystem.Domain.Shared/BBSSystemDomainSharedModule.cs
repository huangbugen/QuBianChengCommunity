using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace BBSSystem.Domain.Shared
{
    [DependsOn(typeof(AbpDddDomainSharedModule))]
    public class BBSSystemDomainSharedModule : AbpModule
    {

    }
}