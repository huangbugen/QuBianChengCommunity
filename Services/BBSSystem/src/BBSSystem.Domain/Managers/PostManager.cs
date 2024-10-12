using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Domain.PostInfo;
using BBSSystem.Domain.Shared.Claims;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace BBSSystem.Domain.Managers
{
    public class PostManager : DomainService
    {
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<PostType> _postTypeRepo;

        public IQueryable<Post> PostQueryable => _postRepo.GetQueryableAsync().Result;

        private readonly ICurrentClaims _currentClaims;

        public PostManager(
            IRepository<Post> postRepo,
            IRepository<PostType> postTypeRepo,
            ICurrentClaims currentClaims
        )
        {
            this._postRepo = postRepo;
            this._postTypeRepo = postTypeRepo;
            this._currentClaims = currentClaims;
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            post.InitPost(_currentClaims);
            post = await _postRepo.InsertAsync(post);

            return post;
        }

        public async Task<List<Post>> GetPostsBySectionAsync(int skip, int size, string sectionId, string typeId)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(sectionId);

            var query = await _postRepo.GetQueryableAsync();
            var posts = await query.Where(m => m.SectionId == sectionId && (string.IsNullOrEmpty(typeId) || typeId == m.PostTypeId)).Skip(skip).Take(size).ToListAsync();

            return await GetPostsInfoAsync(posts.ToArray());
        }

        public async Task<List<Post>> GetPostsInfoAsync([NotNull] params Post[] postArray)
        {
            ArgumentNullException.ThrowIfNull(postArray);
            var posts = postArray.ToList();
            var postTypeIds = posts.Select(m => m.PostTypeId);
            var postTypes = await _postTypeRepo.GetListAsync(m => postTypeIds.Contains(m.Id));

            posts.ForEach(m =>
            {
                m.PostType = postTypes.FirstOrDefault(n => n.Id == m.PostTypeId)!;
            });

            return posts;
        }

        public async Task<int> GetPostCountBySectionAsync(string sectionId, string typeId)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(sectionId);
            var count = await _postRepo.CountAsync(m => m.SectionId == sectionId && typeId == m.PostTypeId);

            return count;
        }

        public async Task<List<PostType>> GetPostTypesAsync(string sectionId)
        {
            var queryable = await _postTypeRepo.GetQueryableAsync();
            var list = await queryable.Where(m => m.SectionId == sectionId).OrderBy(m => m.Order).ToListAsync();
            return list;
        }

        public async Task<List<Post>> GetPostsAsync(int skip, int size, [NotNull] params string[] postIds)
        {
            if (postIds != null && postIds.Length > 0)
            {
                var query = await _postRepo.GetQueryableAsync();
                var posts = query.Where(m => postIds.Contains(m.Id)).Skip(skip).Take(size).ToList();

                return await GetPostsInfoAsync(posts.ToArray());
            }
            else
            {
                throw new ArgumentNullException("postIds参数不能有空或者没有内容");
            }

        }

        public async Task<List<PostUserCount>> GetPostCountByUserIdAsync(params string[] userIds)
        {
            var countGroup = await PostQueryable.Where(m => userIds.Contains(m.CreateUserId)).GroupBy(m => new { m.CreateUserId, m.PostLevelId }, (m, n) => new PostUserCount
            {
                Count = n.Count(),
                UserId = m.CreateUserId,
                PostLevel = m.PostLevelId
            }).ToListAsync();
            return countGroup;
        }

        public class PostUserCount
        {
            public int Count { get; set; }
            public string PostLevel { get; set; }
            public string UserId { get; set; }
        }
        public class PostTypeCount
        {
            public int Count { get; set; }
            public string PostTypeId { get; set; }
        }
    }
}