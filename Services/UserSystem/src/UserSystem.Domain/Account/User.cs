using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace UserSystem.Domain.Account
{
    public class User : AggregateRoot<string>, IHasCreationTime
    {
        public string UserName { get; set; }
        public string UserNo { get; set; }
        public string HeadUrl { get; set; }
        public string UserLevelId { get; set; }
        public DateTime CreationTime { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string ActiveCode { get; set; }

        [NotMapped]
        public List<Role> Roles { get; set; }

        [NotMapped]
        public UserLevel UserLevel { get; set; }

        [NotMapped]
        public UserPassword UserPassword { get; set; }

        public User(string id) : base(id)
        {
            CreationTime = DateTime.Now;
            Status = 0;
            ActiveCode = Guid.NewGuid().ToString("N");
            HeadUrl = "测试";
        }

        public void CreateUser(Level levelNow)
        {
            SetPassword();
            SetUserLevel(levelNow);
        }

        /// <summary>
        /// 配置用户密码
        /// </summary>
        public void SetPassword()
        {
            UserPassword = new();
            UserPassword.CreationTime = DateTime.Now;
            UserPassword.UserId = Id;
            UserPassword.IsDisuse = "F";
        }

        /// <summary>
        /// 配置用户等级
        /// </summary>
        /// <param name="levelNow"></param>
        public void SetUserLevel(Level levelNow)
        {
            UserLevel = new();
            UserLevelId = UserLevel.Id;
            UserLevel.Integral = levelNow.NeedIntegral;
            UserLevel.LevelId = levelNow.Id;
        }
    }
}