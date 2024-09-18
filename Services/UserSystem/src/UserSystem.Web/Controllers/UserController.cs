using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSystem.Application.Contracts.UserApp;
using UserSystem.Application.Contracts.UserApp.Dtos;

namespace UserSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService
        )
        {
            this._userService = userService;
        }

        [HttpPost]
        [TypeFilter(typeof(ValidateAntiForgeryTokenAttribute))]
        public async Task<bool> RegisterUserAsync(UserCreateDto createInput)
        {
            return await _userService.RegisterUserAsync(createInput);
        }

        [HttpGet]
        public async Task<ActionResult<string>> CheckLogin(string userNo, string password)
        {
            var token = await _userService.CheckLoginAsync(userNo, password);
            return token;
        }

        [HttpGet("RefreshToken")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var res = await _userService.RefreshToken();
            if (!res.isSuccess)
            {
                return Unauthorized(res.token);
            }
            else
            {
                return res.token;
            }
        }
    }
}