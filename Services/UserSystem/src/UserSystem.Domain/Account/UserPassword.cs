using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Volo.Abp.Domain.Entities;

namespace UserSystem.Domain.Account
{
    public class UserPassword : Entity<string>
    {
        public string Password { get; set; }
        public string UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string IsDisuse { get; set; }

        public UserPassword()
        {

        }

        public UserPassword(string password)
        {
            Id = Guid.NewGuid().ToString("N");
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}