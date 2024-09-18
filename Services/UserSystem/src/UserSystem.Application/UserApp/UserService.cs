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
        private readonly HttpContext _httpContext;

        public UserService(
            UserManager userManager,
            LevelManager levelManager,
            TokenCreateModel tokenCreateModel,
            IHttpContextAccessor httpContextAccessor,
            IDistributedCache<UserRefreshToken> userRefreshTokenCache
        )
        {
            this._userManager = userManager;
            this._levelManager = levelManager;
            this._tokenCreateModel = tokenCreateModel;
            this._userRefreshTokenCache = userRefreshTokenCache;
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