using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Entity.Core
{
    public class UserClaim
    {
        public int User_Id { get; set; }
        /// <summary>
        /// 多个角色ID
        /// </summary>
        public int Role_Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string UserTrueName { get; set; }
        public int Enable { get; set; }
        public string Token { get; set; }
    }
}