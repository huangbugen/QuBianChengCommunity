using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.Shared.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BBSSystem.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CurrentUserAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ICurrentClaims _currentClaims;

        public CurrentUserAuthorizationFilterAttribute(ICurrentClaims currentClaims)
        {
            this._currentClaims = currentClaims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            _currentClaims.TransClaims(claims);
        }
    }
}