using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.CommonDtos;
using BBSSystem.Application.Contracts.PostApp;
using BBSSystem.Application.Contracts.PostApp.Dtos;
using BBSSystem.Application.Contracts.ReplyApp;
using BBSSystem.Web.Filters;
using BBSSystem.Web.Hubs;
using BBSSystem.Web.Hubs.HubDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BBSSystem.Web.Controllers
{
    [ApiController]
    [Route("bbs/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IReplyService _replyService;
        private readonly IHubContext<MessagingHub> _messagingHub;

        public PostController(
            IPostService postService,
            IReplyService replyService,
            IHubContext<MessagingHub> messagingHub
        )
        {
            this._postService = postService;
            this._replyService = replyService;
            this._messagingHub = messagingHub;
        }

        [HttpPost]
        [Authorize]
        // [TypeFilter(typeof(CurrentUserAuthorizationFilterAttribute))]
        public async Task<bool> AddPostAsync(PostCreateDto createDto)
        {
            var replyCreateDto = createDto.ReplyCreate;
            createDto.ReplyCreate = null;
            var postDto = await _postService.AddPostAsync(createDto);
            replyCreateDto.SectionId = postDto.SectionId;
            replyCreateDto.PostId = postDto.Id;
            var isSuccess = await _replyService.AddReplyAsync(replyCreateDto, true);

            var res = new PostResponseMessageDto
            {
                SectionId = postDto.SectionId,
                Type = PostResponseMessageType.Success
            };
            await _messagingHub.Clients.All.SendAsync("onReceiveMessage", res);

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

        [HttpGet("{postId}/{replyPageIndex}")]
        // [TypeFilter(typeof(CurrentUserAuthorizationFilterAttribute))]
        public async Task<Dictionary<string, object>> GetPostDetailById(string postId, int replyPageIndex = 1, int? replyPageSize = 30)
        {
            var post = await _postService.GetPostInDetailPageDtoAsync(postId);
            var replyRes = await _replyService.GetReplysByPostIdAsync(postId, replyPageIndex, replyPageSize ?? 30);
            var replys = replyRes.replyDtos;
            var users = replyRes.userBBSDtos;
            return new Dictionary<string, object>{
                {"post",post},
                {"replys",replys},
                {"users",users}
            };
        }
    }
}