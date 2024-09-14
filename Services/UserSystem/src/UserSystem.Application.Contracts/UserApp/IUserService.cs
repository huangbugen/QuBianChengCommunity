using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSystem.Application.Contracts.UserApp.Dtos;
using Volo.Abp.Application.Services;

namespace UserSystem.Application.Contracts.UserApp
{
    public interface IUserService : IApplicationService
    {
        Task<bool> RegisterUserAsync(UserCreateDto createInput);
    }
}