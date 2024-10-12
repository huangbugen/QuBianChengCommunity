using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSSystem.Domain.Shared.Ctos
{
    public class UserPostAndReplyCountCto
    {
        public int PostCount { get; set; }
        public int? ReplyCount { get; set; }
        public int EssenceCount { get; set; }
    }
}