using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using UserSystem.Domain.Account;
using UserSystem.Domain.Shared.Enums;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace UserSystem.Domain.Manager
{
    public class UserManager : DomainService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<UserLevel> _userLevelRepo;
        private readonly IRepository<UserRoleMapping> _userRoleMappingRepo;
        private readonly IRepository<Role> _roleRepo;
        private readonly IRepository<Level> _levelRepo;
        private readonly IRepository<UserPassword> _userPasswordRepo;

        public IQueryable<UserLevel> UserLevelQueryable => _userLevelRepo.GetQueryableAsync().Result;

        public UserManager(
            IRepository<User> userRepo,
            IRepository<UserPassword> userPasswordRepo,
            IRepository<UserLevel> userLevelRepo,
            IRepository<UserRoleMapping> userRoleMappingRepo,
            IRepository<Role> roleRepo,
            IRepository<Level> levelRepo
        )
        {
            this._userRepo = userRepo;
            this._userLevelRepo = userLevelRepo;
            this._userRoleMappingRepo = userRoleMappingRepo;
            this._roleRepo = roleRepo;
            this._levelRepo = levelRepo;
            this._userPasswordRepo = userPasswordRepo;
        }

        /// <summary>
        /// 跟据用户Id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<User>> GetUserAsync(params string[] userId)
        {
            // WHERE IN
            var users = await _userRepo.GetListAsync(m => userId.Contains(m.Id));
            return await GetUserInfo(users);
        }

        public async Task<List<UserLevel>> GetLevelIdByUserLevelIdAsync(params string[] userLevelIds)
        {
            var levels = await UserLevelQueryable.Where(m => userLevelIds.Contains(m.Id)).ToListAsync();
            return levels;
        }

        public async Task<Dictionary<string, NextLevelInfo>> GetNextLevelByCurrentLevekAsync(params string[] currentLevelIds)
        {
            Dictionary<string, NextLevelInfo> dicCurrnetLevelMapping = new Dictionary<string, NextLevelInfo>();
            var levels = (await _levelRepo.GetListAsync()).OrderBy(m => m.NeedIntegral);
            foreach (var currentLevelId in currentLevelIds)
            {
                var currentLevel = levels.FirstOrDefault(m => m.Id == currentLevelId);
                var nextLevel = levels.FirstOrDefault(m => m.NeedIntegral > currentLevel.NeedIntegral);
                dicCurrnetLevelMapping.Add(currentLevelId, new NextLevelInfo
                {
                    CurrentLevel = currentLevel,
                    NextLevel = nextLevel
                });
            }

            return dicCurrnetLevelMapping;
        }

        public async Task<bool> HasUserNoAsync(string userNo)
        {
            if (string.IsNullOrWhiteSpace(userNo))
            {
                throw new ArgumentNullException("userNo");
            }

            return await _userRepo.AnyAsync(m => m.UserNo == userNo);
        }

        public async Task<bool> InsertAggregateAsync(User user)
        {
            try
            {
                user = await _userRepo.InsertAsync(user);
                user.UserPassword = await _userPasswordRepo.InsertAsync(user.UserPassword);
                user.UserLevel = await _userLevelRepo.InsertAsync(user.UserLevel);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取登录的用户
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="password"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        public async Task<User> GetLoginUser(string userNo, string password, LoginType loginType)
        {
            // User? user = await _userRepo.GetAsync(m => m.UserNo == userNo);

            User? user = null;

            user = loginType switch
            {
                LoginType.Email => await _userRepo.GetAsync(m => m.Email == userNo),
                _ => await _userRepo.GetAsync(m => m.UserNo == userNo)
            };

            if (user != null)
            {
                var userPassword = await _userPasswordRepo.GetAsync(m => m.UserId == user.Id && m.IsDisuse == "F");
                var isPwdRight = IsPasswordRight(password, userPassword.Password);
                if (isPwdRight)
                {
                    var users = await GetUserInfo(new List<User> { user });
                    return users.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取完整用户信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task<List<User>> GetUserInfo(List<User> users)
        {
            var userIds = users.Select(m => m.Id);
            var userLevelIds = users.Select(m => m.UserLevelId);

            var userLevels = await _userLevelRepo.GetListAsync(m => userLevelIds.Contains(m.Id));
            var userRoles = await _userRoleMappingRepo.GetListAsync(m => userIds.Contains(m.UserId));

            var roleIds = userRoles.Select(m => m.RoleId).Distinct();
            var roles = await _roleRepo.GetListAsync(m => roleIds.Contains(m.Id));

            users.ForEach(m =>
            {
                m.UserLevel = userLevels.FirstOrDefault(n => n.Id == m.UserLevelId);
                var userRoleIds = userRoles.Where(n => n.UserId == m.Id).Select(m => m.RoleId).Distinct();
                m.Roles = roles.FindAll(n => userRoleIds.Contains(n.Id));
            });

            return users;
        }

        private bool IsPasswordRight(string password, string passwordHash)
        {
            // return BCrypt.Net.BCrypt.Verify(password, passwordHash);
            // return password == passwordHash ? true : false;
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public class NextLevelInfo
        {
            public Level CurrentLevel { get; set; }
            public Level NextLevel { get; set; }
        }
    }
}