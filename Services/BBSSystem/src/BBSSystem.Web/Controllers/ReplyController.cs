using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Application.Contracts.ReplyApp;
using BBSSystem.Application.Contracts.ReplyApp.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BBSSystem.Web.Controllers
{
    [ApiController]
    [Route("bbs/[controller]")]
    public class ReplyController : ControllerBase
    {
        private readonly IReplyService _replyService;
        public ReplyController(IReplyService replyService)
        {
            this._replyService = replyService;
        }

        [HttpPost]
        public async Task<bool> AddReplyAsync(ReplyCreateOnlyDto createOnlyDto)
        {
            return await _replyService.AddReplyAsync(createOnlyDto);
        }

        [HttpPut]
        public async Task<bool> UpdateReplyContentAsync(string replyId, ReplyUpdateContentDto contentDto)
        {
            return await _replyService.UpdateReplyContentAsync(replyId, contentDto);
        }
    }
}