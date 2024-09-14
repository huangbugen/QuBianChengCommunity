using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace UserSystem.Application.Contracts
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule)
    )]
    public class UserSystemApplicationContractsModule : AbpModule
    {

    }
}