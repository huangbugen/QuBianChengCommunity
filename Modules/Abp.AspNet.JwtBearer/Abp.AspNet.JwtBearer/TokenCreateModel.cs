using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Abp.AspNet.JwtBearer
{
    public class TokenCreateModel
    {
        private readonly IConfiguration _configuration;

        public string UserId { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Ts { get; set; }

        public TokenCreateModel(
            IConfiguration configuration
        )
        {
            this._configuration = configuration;
        }

        
    }
}