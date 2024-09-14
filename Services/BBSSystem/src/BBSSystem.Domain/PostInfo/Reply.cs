using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.Shared.Claims;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace BBSSystem.Domain.PostInfo
{
    public class Reply : Entity<string>
    {
        public Reply(string id) : base(id)
        {

        }

        public string ReplyContent { get; set; }
        public string UserId { get; set; }
        public string HeadUrl { get; set; }
        public string UserName { get; set; }
        public string SectionId { get; set; }
        public DateTime CreationTime { get; set; }
        public string IsClose { get; set; }
        public string? CloseUserId { get; set; }
        public string PostId { get; set; }
        [Comment("是否是帖子内容（T/F）")]
        public string IsMasterReply { get; set; } = "F";
        [Comment("是否已审核（T/F）")]
        public string IsReview { get; set; } = "T";
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public string? QuoteReplyId { get; set; }
        public string? QuoteReplyUserId { get; set; }
        public string? QuoteReplyContent { get; set; }

        public void InitReplay(ICurrentClaims currentClaims, bool isMaster)
        {
            SetIsMasterValue(isMaster);
            UserId = currentClaims.UserId;
            UserName = currentClaims.UserName;
            HeadUrl = currentClaims.HeadUrl;
            IsClose = "F";
        }

        public void InitReplay(bool isMaster)
        {
            SetIsMasterValue(isMaster);
            UserId = "123456";
            UserName = "黄步根";
            HeadUrl = "http";
            IsClose = "F";
        }

        public void SetIsMasterValue(bool isMaster)
        {
            IsMasterReply = isMaster ? "T" : "F";
        }
    }
}