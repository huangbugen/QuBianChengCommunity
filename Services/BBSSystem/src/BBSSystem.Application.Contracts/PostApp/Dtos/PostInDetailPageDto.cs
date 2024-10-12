using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBSSystem.Application.Contracts.PostApp.Dtos
{
    public class PostInDetailPageDto : EntityDto<string>
    {
        public string PostTitle { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string PostTypeName { get; set; }
        public long ReplyTimes { get; set; }
        public long BrowseTimes { get; set; }
        public string IsClose { get; set; }
    }
}