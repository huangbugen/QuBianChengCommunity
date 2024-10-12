using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSSystem.Web.Hubs.HubDto
{
    public class PostResponseMessageDto
    {
        public string SectionId { get; set; }
        public PostResponseMessageType Type { get; set; }
    }

    public enum PostResponseMessageType
    {
        Success = 1,
        Err = 2
    }
}