using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.ReplyApp;
using BBSSystem.Application.Contracts.ReplyApp.Dtos;
using BBSSystem.Domain.Managers;
using BBSSystem.Domain.PostInfo;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.ReplyApp
{
    public class ReplyService : ApplicationService, IReplyService
    {
        private readonly ReplyManager _replyManager;

        public ReplyService(
            ReplyManager replyManager
        )
        {
            this._replyManager = replyManager;
        }

        public async Task<bool> AddReplyAsync(ReplyCreateDto createDto, bool isMaster)
        {
            var reply = ObjectMapper.Map<ReplyCreateDto, Reply>(createDto);
            var res = await _replyManager.AddReplyAsync(reply, isMaster);

            return true;
        }

        public async Task<bool> AddReplyAsync(ReplyCreateOnlyDto createOnlyDto)
        {
            var reply = ObjectMapper.Map<ReplyCreateOnlyDto, Reply>(createOnlyDto);
            await _replyManager.AddReplyAsync(reply, false);
            return true;
        }

        public async Task<bool> UpdateReplyContentAsync(string replyId, ReplyUpdateContentDto contentDto)
        {
            var reply = await _replyManager.GetReplyAsync(replyId);
            reply = ObjectMapper.Map(contentDto, reply);
            await _replyManager.UpdateReplyAsync(reply);
            return true;
        }
    }
}