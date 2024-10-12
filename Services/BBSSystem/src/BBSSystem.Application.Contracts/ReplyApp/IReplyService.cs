using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.ReplyApp.Dtos;
using UserSystem.Application.Contracts.UserApp.Dtos;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.Contracts.ReplyApp
{
    public interface IReplyService : IApplicationService
    {
        Task<bool> AddReplyAsync(ReplyCreateDto createDto, bool isMaster);

        Task<bool> UpdateReplyContentAsync(string replyId, ReplyUpdateContentDto contentDto);

        Task<bool> AddReplyAsync(ReplyCreateOnlyDto createOnlyDto);

        Task<(List<UserBBSDto> userBBSDtos, List<ReplyDto> replyDtos)> GetReplysByPostIdAsync(string postId, int pageIndex, int pageSize);
    }
}