using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSystem.Domain.Shared.Models
{
    public class UserRefreshToken
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}