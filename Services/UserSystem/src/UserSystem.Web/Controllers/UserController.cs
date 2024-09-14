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
        public async Task<bool> RegisterUserAsync(UserCreateDto createInput)
        {
            return await _userService.RegisterUserAsync(createInput);
        }
    }
}