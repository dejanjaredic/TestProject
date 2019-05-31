using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.IdentityServer4;
using Abp.Modules;

namespace Proba
{
    [DependsOn(typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule))]
    public class ProbaModel : AbpModule
    {
    }
}
