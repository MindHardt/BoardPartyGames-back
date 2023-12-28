using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Hubs;

public class ChatHub : Hub
{
    public async Task JoinChat(string chatName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
    }

    public async Task SendMessage(string chatName, string user, string message)
    {
        await Clients.Group(chatName).SendAsync("ReceiveMessage", user, message);
    }
}