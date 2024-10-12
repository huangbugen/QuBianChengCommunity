using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileDownLoadSystem.Core.Filters
{
    public class ActionPermissionFilterAttribute : IAsyncActionFilter
    {
        public ActionPermissionFilterAttribute(ActionPermissionRequirement requirement, UserContext userContext)
        {
            Requirement = requirement;
            UserContext = userContext;
        }

        public ActionPermissionRequirement Requirement { get; }
        public UserContext UserContext { get; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (Requirement.RoleId.Contains(UserContext.RoleId))
            {
                var res = await UserContext.GetPermissions(UserContext.RoleId);
                if (res.Any(m => m.ActionName == Requirement.TableName))
                {
                    await next();
                }
            }

        }
    }
}