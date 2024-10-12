using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.PostInfo;
using BBSSystem.Domain.Shared.Claims;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Reply> ReplyQueryable => _replyRepo.GetQueryableAsync().Result;

        private readonly ICurrentClaims _currentClaims;

        public ReplyManager(
        IRepository<Reply> replyRepo,
        IDataFilter dataFilter,
        ICurrentClaims currentClaims
        )
        {
            this._replyRepo = replyRepo;
            this._dataFilter = dataFilter;
            this._currentClaims = currentClaims;
        }

        public async Task<Reply> AddReplyAsync(Reply reply, bool isMaster)
        {
            // reply.InitReplay(isMaster);
            reply.InitReplay(_currentClaims, isMaster);
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

        public async Task<List<ReplyUserCount>> GetReplyCountByUserIdAsync(params string[] userIds)
        {
            var countGroup = await ReplyQueryable.Where(m => userIds.Contains(m.UserId)).GroupBy(m => m.UserId, (m, n) => new ReplyUserCount
            {
                Count = n.Count(),
                UserId = m
            }).ToListAsync();
            return countGroup;
        }

        public async Task<Reply> UpdateReplyAsync(Reply reply)
        {
            return await _replyRepo.UpdateAsync(reply);
        }

        public async Task<List<Reply>> GetReplyByPostIdAsync(string postId, int pageIndex = 1, int pageSize = 30)
        {
            return await ReplyQueryable.Where(m => m.PostId == postId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        public class ReplyUserCount
        {
            public int Count { get; set; }
            public string UserId { get; set; }
        }
    }
}