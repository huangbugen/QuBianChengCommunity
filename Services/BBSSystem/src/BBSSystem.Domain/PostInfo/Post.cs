using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.Shared.Claims;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace BBSSystem.Domain.PostInfo
{
    public class Post : Entity<string>
    {
        public Post(string id) : base(id)
        {

        }

        public string PostTitle { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public string PostTypeId { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastReplyUserId { get; set; }
        public string LastReplyUserName { get; set; }
        public DateTime LastReplyDate { get; set; }
        public long ReplyTimes { get; set; }
        public long BrowseTimes { get; set; }
        public string PostLevelId { get; set; }
        public string TopTypeId { get; set; }
        public string IsClose { get; set; }
        public string CloseUserId { get; set; }
        [Comment("是否已审核（T/F）")]
        public string IsReview { get; set; } = "T";
        public PostType PostType { get; set; }

        // public void InitPost(ICurrentClaims claims)
        // {
        //     CreateUserId = claims.UserId;
        //     CreateUserName = claims.UserName;
        //     CreateDate = DateTime.Now;
        //     LastReplyUserId = claims.UserId;
        //     LastReplyUserName = claims.UserName;
        //     LastReplyDate = CreateDate;
        //     BrowseTimes = 0;
        //     ReplyTimes = 0;
        //     PostLevelId = "";
        //     TopTypeId = "";
        //     IsClose = "F";
        //     CloseUserId = "";
        // }

        public void InitPost(ICurrentClaims claims)
        {
            CreateUserId = claims.UserId;
            // CreateUserId = "claims.UserId";
            CreateUserName = claims.UserName;
            // CreateUserName = "claims.UserName";
            CreateDate = DateTime.Now;
            LastReplyUserId = claims.UserId;
            // LastReplyUserId = "claims.UserId";
            LastReplyUserName = claims.UserName;
            // LastReplyUserName = "claims.UserName";
            LastReplyDate = CreateDate;
            BrowseTimes = 0;
            ReplyTimes = 0;
            PostLevelId = "";
            TopTypeId = "";
            IsClose = "F";
            CloseUserId = "";
        }
    }
}