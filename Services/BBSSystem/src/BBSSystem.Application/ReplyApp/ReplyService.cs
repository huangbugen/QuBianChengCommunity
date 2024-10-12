using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.ReplyApp;
using BBSSystem.Application.Contracts.ReplyApp.Dtos;
using BBSSystem.Domain.Managers;
using BBSSystem.Domain.PostInfo;
using BBSSystem.Domain.Shared.Ctos;
using Microsoft.Extensions.Caching.Distributed;
using UserSystem.Application.Contracts.UserApp;
using UserSystem.Application.Contracts.UserApp.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using static BBSSystem.Domain.Managers.PostManager;
using static BBSSystem.Domain.Managers.ReplyManager;

namespace BBSSystem.Application.ReplyApp
{
    public class ReplyService : ApplicationService, IReplyService
    {
        private readonly ReplyManager _replyManager;
        private readonly IUserService _userService;
        private readonly IDistributedCache<UserPostAndReplyCountCto> _userPostAndReplyCountCache;
        private readonly PostManager _postManager;

        public ReplyService(
            ReplyManager replyManager,
            IUserService userService,
            IDistributedCache<UserPostAndReplyCountCto> userPostAndReplyCountCache,
            PostManager postManager
        )
        {
            this._replyManager = replyManager;
            this._userService = userService;
            this._userPostAndReplyCountCache = userPostAndReplyCountCache;
            this._postManager = postManager;
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

        public async Task<(List<UserBBSDto> userBBSDtos, List<ReplyDto> replyDtos)> GetReplysByPostIdAsync(string postId, int pageIndex, int pageSize)
        {
            var replys = await _replyManager.GetReplyByPostIdAsync(postId, pageIndex, pageSize);
            var userIds = replys.Select(m => m.UserId).ToArray();
            var userBBSDtos = await GetUserBBSDtoByUserIds(userIds);
            var replyDtos = ObjectMapper.Map<List<Reply>, List<ReplyDto>>(replys);
            return (userBBSDtos, replyDtos);
        }

        private async Task<List<UserBBSDto>> GetUserBBSDtoByUserIds(string[] userIds)
        {
            // 从UserSystem中拿到用户信息
            var userBBSDtos = await _userService.GetUserBBSDtosAsync(userIds);
            // 从缓存中获取
            var userKeys = userIds.Select(m => $"UserPostAndReplyCount:{m}");
            var cacheValues = _userPostAndReplyCountCache.GetMany(userKeys);
            // 拿到缓存中存在的用户信息
            var valusRes = cacheValues.Where(m => m.Value != null).Select(m => m.Value);
            // 拿到缓存中不存在用户信息的Id
            userIds = cacheValues.Where(m => m.Value == null).Select(m => m.Key.Substring(m.Key.IndexOf(":") + 1)).ToArray();

            List<PostUserCount> userPostCount = null;
            List<ReplyUserCount> userReplyCount = null;

            if (userIds.Length > 0)
            {
                // 计算用户发帖数
                userPostCount = await _postManager.GetPostCountByUserIdAsync(userIds);
                // 计算用户回帖数
                userReplyCount = await _replyManager.GetReplyCountByUserIdAsync(userIds);
            }

            // 结合用户发帖数据
            userBBSDtos.ForEach(user =>
            {
                var cacheValue = cacheValues.FirstOrDefault(m => m.Key == "UserPostAndReplyCount:" + user.Id).Value;
                if (cacheValue != null)
                {
                    user.PostCount = cacheValue.PostCount;
                    user.ReplyCount = cacheValue.ReplyCount;
                    user.EssenceCount = cacheValue.EssenceCount;
                }
                else
                {
                    user.PostCount = userPostCount.Where(m => m.UserId == user.Id).Sum(m => m.Count);
                    user.ReplyCount = userPostCount.FirstOrDefault(m => m.UserId == user.Id)?.Count;
                    user.EssenceCount = userPostCount.Where(m => m.UserId == user.Id && m.PostLevel == "889e66c1b7bc4868b880b0dc00d5b40d").Sum(m => m.Count);
                    _userPostAndReplyCountCache.Set($"UserPostAndReplyCount:{user.Id}", new UserPostAndReplyCountCto
                    {
                        EssenceCount = user.EssenceCount,
                        ReplyCount = user.ReplyCount,
                        PostCount = user.PostCount
                    }, new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromDays(30)
                    });
                }
            });
            return userBBSDtos;
        }
    }
}