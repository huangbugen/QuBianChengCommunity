using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBSSystem.Application.Contracts.ReplyApp.Dtos
{
    public class ReplyDto : EntityDto<string>
    {
        public string ReplyContent { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string HeadUrl { get; set; }
        public string SectionId { get; set; }
        public DateTime CreationTime { get; set; }
        public string IsClose { get; set; }
        public string PostId { get; set; }
        public string IsMasterReply { get; set; } = "F";
        public DateTime? LastModificationTime { get; set; }

        public string QuoteReplyId { get; set; }
        public string QuoteReplyUserId { get; set; }
        public string QuoteReplyContent { get; set; }
    }
}