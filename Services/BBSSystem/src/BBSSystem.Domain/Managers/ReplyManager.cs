using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.PostInfo;
using BBSSystem.Domain.Shared.Claims;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace BBSSystem.Domain.Managers
{
    public class ReplyManager : DomainService
    {
        private readonly IRepository<Reply> _replyRepo;
        private readonly IDataFilter _dataFilter;

        // private readonly ICurrentClaims _currentClaims;

        public ReplyManager(
        IRepository<Reply> replyRepo,
        IDataFilter dataFilter
        // ICurrentClaims currentClaims
        )
        {
            this._replyRepo = replyRepo;
            this._dataFilter = dataFilter;
            // this._currentClaims = currentClaims;
        }

        public async Task<Reply> AddReplyAsync(Reply reply, bool isMaster)
        {
            reply.InitReplay(isMaster);
            var res = await _replyRepo.InsertAsync(reply);

            return res;
        }

        public async Task<Reply> GetReplyAsync(string replyId, bool isDisableSoftDelete = false)
        {
            Reply reply = null;

            if (isDisableSoftDelete)
            {
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    reply = await _replyRepo.FirstOrDefaultAsync(m => m.Id == replyId);
                }
            }
            else
            {
                reply = await _replyRepo.FirstOrDefaultAsync(m => m.Id == replyId);
            }

            return reply;
        }

        public async Task<Reply> UpdateReplyAsync(Reply reply)
        {
            return await _replyRepo.UpdateAsync(reply);
        }
    }
}