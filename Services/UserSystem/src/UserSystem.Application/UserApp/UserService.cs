using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSystem.Application.Contracts.UserApp;
using UserSystem.Application.Contracts.UserApp.Dtos;
using UserSystem.Domain.Account;
using UserSystem.Domain.Manager;
using UserSystem.Domain.Shared.Enums;
using Volo.Abp.Application.Services;

namespace UserSystem.Application.UserApp
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly UserManager _userManager;
        private readonly LevelManager _levelManager;

        public UserService(
            UserManager userManager,
        LevelManager levelManager
        )
        {
            this._userManager = userManager;
            this._levelManager = levelManager;
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
                user.CreateUser(levelNow);
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
            var user = await _userManager.GetLoginUser(userNo, password, LoginType.UserNo);
            string token = "";
            if (user != null)
            {
                token =
            }
        }
    }
}