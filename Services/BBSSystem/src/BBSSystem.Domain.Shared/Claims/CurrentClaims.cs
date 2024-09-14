using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BBSSystem.Domain.Shared.Claims
{
    public class CurrentClaims : ICurrentClaims
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string HeadUrl { get; set; }

        public void TransClaims(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case "userId":
                        {
                            UserId = claim.Value;
                            break;
                        }
                    case "userName":
                        {
                            UserName = claim.Value;
                            break;
                        }
                    case "headUrl":
                        {
                            HeadUrl = claim.Value;
                            break;
                        }
                }
            }
        }
    }
}