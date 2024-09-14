using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BBSSystem.Domain.Shared.Claims
{
    public interface ICurrentClaims : IScopedDependency
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string HeadUrl { get; set; }
        void TransClaims(IEnumerable<Claim> claims);
    }
}