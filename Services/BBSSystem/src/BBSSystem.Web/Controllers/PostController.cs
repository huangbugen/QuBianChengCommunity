using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.CommonDtos;
using BBSSystem.Application.Contracts.PostApp;
using BBSSystem.Application.Contracts.PostApp.Dtos;
using BBSSystem.Application.Contracts.ReplyApp;
using Microsoft.AspNetCore.Mvc;

namespace BBSSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IReplyService _replyService;

        public PostController(
            IPostService postService,
            IReplyService replyService
        )
        {
            this._postService = postService;
            this._replyService = replyService;
        }

        [HttpPost]
        public async Task<bool> AddPostAsync(PostCreateDto createDto)
        {
            var replyCreateDto = createDto.ReplyCreate;
            createDto.ReplyCreate = null;
            var postDto = await _postService.AddPostAsync(createDto);
            replyCreateDto.SectionId = postDto.SectionId;
            replyCreateDto.PostId = postDto.Id;
            var isSuccess = await _replyService.AddReplyAsync(replyCreateDto, true);

            return isSuccess;
        }

        [HttpGet]
        public async Task<ListDto<PostListDisplayDto>> GetPostDtosAsync(string sectionId, string? typeId, int pageIndex = 1, int pageSize = 30)
        {
            return await _postService.GetPostListDisplayDtosAsync(sectionId, typeId, pageIndex, pageSize);
        }

        [HttpGet("PostType")]
        public async Task<List<PostTypeDto>> GetPostTypeDtoAsync(string sectionId)
        {
            return await _postService.GetPostTypeDtoAsync(sectionId);
        }
    }
}