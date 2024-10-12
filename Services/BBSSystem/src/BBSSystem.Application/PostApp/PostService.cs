using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.CommonDtos;
using BBSSystem.Application.Contracts.PostApp;
using BBSSystem.Application.Contracts.PostApp.Dtos;
using BBSSystem.Domain.Managers;
using BBSSystem.Domain.PostInfo;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.PostApp
{
    public class PostService : ApplicationService, IPostService
    {
        private readonly PostManager _postManager;

        public PostService(
            PostManager postManager
        )
        {
            this._postManager = postManager;
        }

        public async Task<PostDto> AddPostAsync(PostCreateDto createDto)
        {
            var post = ObjectMapper.Map<PostCreateDto, Post>(createDto);
            var res = await _postManager.AddPostAsync(post);

            return ObjectMapper.Map<Post, PostDto>(res);
        }

        public async Task<ListDto<PostListDisplayDto>> GetPostListDisplayDtosAsync(string sectionId, string? typeId, int pageIndex, int pageSize)
        {
            var skip = (pageIndex - 1) * pageSize;
            var res = await _postManager.GetPostsBySectionAsync(skip, pageSize, sectionId, typeId);
            var dtos = ObjectMapper.Map<List<Post>, List<PostListDisplayDto>>(res);
            var count = await _postManager.GetPostCountBySectionAsync(sectionId, typeId);

            var resDto = new ListDto<PostListDisplayDto>
            {
                Count = count,
                List = dtos
            };

            return resDto;
        }

        public async Task<List<PostTypeDto>> GetPostTypeDtoAsync(string sectionId)
        {
            var list = await _postManager.GetPostTypesAsync(sectionId);
            var dtos = ObjectMapper.Map<List<PostType>, List<PostTypeDto>>(list);
            return dtos;
        }

        public async Task<PostInDetailPageDto> GetPostInDetailPageDtoAsync(string postId)
        {
            var post = (await _postManager.GetPostsAsync(0, 1, postId)).FirstOrDefault();
            var dto = ObjectMapper.Map<Post, PostInDetailPageDto>(post);
            return dto;
        }
    }
}