using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBSSystem.Web.Hubs.HubDto;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.AspNetCore.SignalR;

namespace BBSSystem.Web.Hubs
{
    [HubRoute("/bbs/signalr-hubs/messaging")]
    public class MessagingHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var cid = Context.ConnectionId;
            // 第一个参数就是前端接受的方法，第二个参数是返回的内容
            Clients.Client(cid).SendAsync("onConnectionAsync", "哈罗啊，饭已OK啦，下来米西吧！");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // 这里可以考虑下线后的一些操作
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string sectionId, PostResponseMessageType type)
        {
            var res = new PostResponseMessageDto
            {
                SectionId = sectionId,
                Type = type
            };
            await Clients.All.SendAsync("onReceiveMessage", res);
        }
    }
}