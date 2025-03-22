using Microsoft.AspNetCore.SignalR;

namespace Client
{
    public sealed class ChatHub : Hub
    {
        private static Dictionary<string, string> ConnectedUsers = new();

        public async Task SendMessage(string sender, string receiver, string message)
        {
            if (ConnectedUsers.TryGetValue(receiver, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", sender, message);
            }
        }

        public async Task SendToAll(string message)
        {
                await Clients.All.SendAsync("ReceiveMessage",  message);
        }

        public override async Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext()?.Request.Query["userName"];
            if (!string.IsNullOrEmpty(userName))
            {
                ConnectedUsers[userName] = Context.ConnectionId;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = ConnectedUsers.FirstOrDefault(u => u.Value == Context.ConnectionId);
            if (!string.IsNullOrEmpty(user.Key))
            {
                ConnectedUsers.Remove(user.Key);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
