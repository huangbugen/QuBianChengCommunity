using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Core.Filters
{
    public class ApiActionPermissionAttribute : ActionPermissionAttribute
    {
        public ApiActionPermissionAttribute() : base(true)
        {

        }
        public ApiActionPermissionAttribute(string tableName, string roleIds) : base(tableName, roleIds, true)
        {

        }
        public ApiActionPermissionAttribute(string roleIds) : base(roleIds, true)
        {

        }
    }
}