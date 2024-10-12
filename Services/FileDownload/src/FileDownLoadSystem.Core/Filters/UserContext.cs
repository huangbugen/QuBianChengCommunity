using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.EfDbContext;
using FileDownLoadSystem.Entity.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FileDownLoadSystem.Core.Filters
{
    public class UserContext
    {
        public UserContext(FileDownloadSystemDbContext context)
        {
            Context = context;
        }

        public FileDownloadSystemDbContext Context { get; }

        public int UserId { get; set; }
        public void GetUserId(HttpContext context)
        {
            var userId = context.User.FindFirst(JwtRegisteredClaimNames.Jti)
                            ?? context.User.FindFirst(ClaimTypes.NameIdentifier);
            _ = int.TryParse(userId?.ToString(), out int _number);
            UserId = _number;
        }

        public int RoleId => UserInfo.RoleId;

        public UserInfo UserInfo => new()
        {
            UserId = 1,
            RoleId = 2
        };

        public async Task<List<Authorization>> GetPermissions(int roleId)
        {
            var authIds = await Context.Set<RoleAuthorizationMapping>()
            .Where(m=> m.RoleId == roleId).Select(m=>m.AuthorizationId).ToListAsync();
            var actions = await Context.Set<Authorization>().Where(m=>authIds.Contains(m.Id) && !m.IsMenu)
            .ToListAsync();
            return actions;
        }

    }
}