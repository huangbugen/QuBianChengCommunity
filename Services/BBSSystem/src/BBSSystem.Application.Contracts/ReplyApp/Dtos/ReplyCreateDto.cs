using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSSystem.Application.Contracts.ReplyApp.Dtos
{
    public class ReplyCreateDto
    {
        public string ReplyContent { get; set; }
        public string? SectionId { get; set; }
        public string? PostId { get; set; }
        public string? IsMasterReply { get; set; } = "F";
    }
}