using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace UserSystem.Application.Contracts.UserApp.Dtos
{
    public class UserLevelDto : EntityDto<string>
    {
        public int Integral { get; set; }
        public string LevelId { get; set; }
    }
}