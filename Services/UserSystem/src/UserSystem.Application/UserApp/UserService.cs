using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.AspNet.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using NUglify.Helpers;
using UserSystem.Application.Contracts.UserApp;
using UserSystem.Application.Contracts.UserApp.Dtos;
using UserSystem.Domain.Account;
using UserSystem.Domain.Manager;
using UserSystem.Domain.Shared.Enums;
using UserSystem.Domain.Shared.Models;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;

namespace UserSystem.Application.UserApp
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly UserManager _userManager;
        private readonly LevelManager _levelManager;
        private readonly TokenCreateModel _tokenCreateModel;
        private readonly IDistributedCache<UserRefreshToken> _userRefreshTokenCache;
        private readonly IDistributedCache<UserBBSDto> _bbsCache;
        private readonly HttpContext _httpContext;

        public UserService(
            UserManager userManager,
            LevelManager levelManager,
            TokenCreateModel tokenCreateModel,
            IHttpContextAccessor httpContextAccessor,
            IDistributedCache<UserRefreshToken> userRefreshTokenCache,
            IDistributedCache<UserBBSDto> bbsCache
        )
        {
            this._userManager = userManager;
            this._levelManager = levelManager;
            this._tokenCreateModel = tokenCreateModel;
            this._userRefreshTokenCache = userRefreshTokenCache;
            this._bbsCache = bbsCache;
            this._httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<bool> RegisterUserAsync(UserCreateDto createInput)
        {
            // 通过领域服务判断是否已存在UserNo
            if (!await _userManager.HasUserNoAsync(createInput.UserNo))
            {
                // 设置用户
                var user = ObjectMapper.Map<UserCreateDto, User>(createInput);
                // 获取初始化等级
                var levels = await _levelManager.GetTop2LevelAsync();
                var levelNow = levels.FirstOrDefault();
                var levelNext = levels[1];
                // 初始化用户信息
                user.CreateUser(levelNow!, createInput.Password);
                // 将信息存入数据库
                await _userManager.InsertAggregateAsync(user);

                // TODO 发邮件
                // EMailSender.SendMail(new SendInfo{});
                return true;
            }

            return false;
        }

        public async Task<List<UserBBSDto>> GetUserBBSDtosAsync(params string[] userIds)
        {
            // 从缓存中获取数据
            var userBBSInfoKeys = new List<string>();
            userIds.ForEach(m => userBBSInfoKeys.Add("userBBSInfo:" + m));
            var cacheRes = _bbsCache.GetMany(userBBSInfoKeys);
            // 拿到缓存中存在的用户信息
            var valueRes = cacheRes.Where(m => m.Value != null).Select(m => m.Value);
            // 拿到缓存中不存在用户信息的Id
            userIds = cacheRes.Where(m => m.Value == null).Select(m => m.Key[(m.Key.IndexOf(':') + 1)..]).ToArray();

            var userBBSDtos = new List<UserBBSDto>();
            if (userIds.Length > 0)
            {
                // 将不存在用户数据的Id拿到数据库中去获取数据
                var users = await _userManager.GetUserAsync(userIds);
                userBBSDtos = ObjectMapper.Map(users, userBBSDtos);
                var userLevelIds = users.Select(m => m.UserLevelId).Distinct().ToList();
                // 拿到UserLevel信息
                var userLevels = await _userManager.GetLevelIdByUserLevelIdAsync(userLevelIds.ToArray());
                // 将 UserLevel信息当前分数拼接到UserBBSDto中
                userBBSDtos.ForEach(m =>
                {
                    var userLevel = userLevels.FirstOrDefault(n => n.Id == m.UserLevelId);
                    m.CurrentIntegral = userLevel.Integral;
                    m.LevelId = userLevel.LevelId;
                });

                // 获取所有需要的CurrentUserId
                var levelIds = userLevels.Select(m => m.LevelId).ToList();
                // 拿到所有的用户下一等级信息
                var dicCurrentLevelNextLevelMapping = await _userManager.GetNextLevelByCurrentLevekAsync(levelIds.ToArray());
                // 获取用户下一等级信息
                userBBSDtos.ForEach(m =>
                {
                    var nextIntegral = dicCurrentLevelNextLevelMapping[m.LevelId].NextLevel;
                    m.NextIntegral = nextIntegral.NeedIntegral;
                    m.LevelName = dicCurrentLevelNextLevelMapping[m.LevelId].CurrentLevel.LevelName;
                });

                List<KeyValuePair<string, UserBBSDto>> redisValue = new List<KeyValuePair<string, UserBBSDto>>();
                userBBSDtos.ForEach(m =>
                {
                    redisValue.Add(new KeyValuePair<string, UserBBSDto>($"userBBSInfo:{m.Id}", m));
                });
                // 将每个用户信息存入Redis
                _bbsCache.SetMany(redisValue);
            }
            // 将缓存中的数据和数据库中拿到数据合并返回
            userBBSDtos.AddRange(valueRes.ToList());
            return userBBSDtos;
        }

        public async Task<string> CheckLoginAsync(string userNo, string password)
        {
            var loginType = GetLoginType(userNo);
            string token = "";
            var user = await _userManager.GetLoginUser(userNo, password, loginType);
            if (user != null)
            {
                token = _tokenCreateModel.GetToken
                (
                    user.Id,
                    new Claim("userName", user.UserName),
                    new Claim("headUrl", user.HeadUrl)
                );
                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken, user.Id);
            }
            return token;
        }

        public async Task<(string token, bool isSuccess)> RefreshToken()
        {
            var cookieRefreshToken = _httpContext.Request.Cookies["refreshToken"];
            var value = await _userRefreshTokenCache.GetAsync(cookieRefreshToken);
            if (value != null)
            {
                var userRefreshToken = value;
                if (userRefreshToken.Token != cookieRefreshToken)
                {
                    return ("refreshToken 验证失败", false);
                }
                else if (userRefreshToken.TokenExpires < DateTime.Now)
                {
                    return ("refreshToken 已过期", false);
                }

                var token = _tokenCreateModel.GetToken(userRefreshToken.UserId);
                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken, userRefreshToken.UserId);
                return (token, true);
            }
            return ("当前用户不存在", false);
        }

        private LoginType GetLoginType(string loginNo)
        {
            Regex regexEmail = new Regex("/^(([^<>()[\\]\\.,;:\\s@\"]+(\\.[^<>()[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$/");
            if (regexEmail.IsMatch(loginNo))
            {
                return LoginType.Email;
            }
            return LoginType.UserNo;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7)
            };

            return refreshToken;
        }

        private async void SetRefreshToken(RefreshToken refreshToken, string userId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            _httpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            var userRefreshToken = new UserRefreshToken
            {
                UserId = userId,
                Token = refreshToken.Token,
                TokenCreated = refreshToken.Created,
                TokenExpires = refreshToken.Expires
            };

            await _userRefreshTokenCache.SetAsync(refreshToken.Token, userRefreshToken);
        }
    }
}