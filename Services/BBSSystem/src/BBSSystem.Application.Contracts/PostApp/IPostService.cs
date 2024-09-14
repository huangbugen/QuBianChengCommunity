using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.CommonDtos;
using BBSSystem.Application.Contracts.PostApp.Dtos;
using Volo.Abp.Application.Services;

namespace BBSSystem.Application.Contracts.PostApp
{
    public interface IPostService : IApplicationService
    {
        Task<PostDto> AddPostAsync(PostCreateDto createDto);
        Task<ListDto<PostListDisplayDto>> GetPostListDisplayDtosAsync(string sectionId, string? typeId, int pageIndex, int pageSize);
        Task<List<PostTypeDto>> GetPostTypeDtoAsync(string sectionId);
    }
}