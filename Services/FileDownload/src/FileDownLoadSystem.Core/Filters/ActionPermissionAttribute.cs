using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FileDownLoadSystem.Core.Filters
{
    public class ActionPermissionAttribute : TypeFilterAttribute
    {
        public ActionPermissionAttribute(bool isApi = false) : base(typeof(ActionPermissionFilterAttribute))
        {
            SetActionPermissionRequirement(null, null, isApi);
        }
        public ActionPermissionAttribute(string roleIds, bool isApi = false) : base(typeof(ActionPermissionFilterAttribute))
        {
            SetActionPermissionRequirement(null, roleIds, isApi);
        }
        public ActionPermissionAttribute(string tableName, string roleIds, bool isApi = false) : base(typeof(ActionPermissionFilterAttribute))
        {
            SetActionPermissionRequirement(tableName, roleIds, isApi);
        }

        private void SetActionPermissionRequirement(string tableName, string roleIds, bool isApi)
        {
            Arguments = new object[]{
                new ActionPermissionRequirement{
                    IsApi = isApi,
                    TableName = tableName,
                    RoleId = (roleIds??"").Split(",").Select(m=>m.GetInt()).ToArray()
                }
            };
        }
    }
}