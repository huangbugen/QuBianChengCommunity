using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace UserSystem.Application.Contracts.UserApp.Dtos
{
    public class UserBBSDto : EntityDto<string>
    {
        public string UserName { get; set; }
        public string UserLevelId { get; set; }
        public string LevelName { get; set; }
        public string LevelId { get; set; }
        public int CurrentIntegral { get; set; }
        public int NextIntegral { get; set; }
        public DateTime CreationTime { get; set; }
    }
}