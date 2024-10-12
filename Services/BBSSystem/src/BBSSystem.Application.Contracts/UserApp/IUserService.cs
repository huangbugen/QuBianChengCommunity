using UserSystem.Application.Contracts.UserApp.Dtos;
using Volo.Abp.Application.Services;

namespace UserSystem.Application.Contracts.UserApp
{
    public interface IUserService : IApplicationService
    {
        Task<List<UserBBSDto>> GetUserBBSDtosAsync(params string[] userIds);
    }
}