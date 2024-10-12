using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Core.Filters
{
    public class ActionPermissionRequirement
    {
        public string TableName { get; set; }
        public string TableAction { get; set; }

        public int[] RoleId { get; set; }

        public bool IsApi { get; set; }

    }
}