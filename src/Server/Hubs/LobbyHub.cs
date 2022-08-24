using Microsoft.AspNetCore.SignalR;

namespace KingSkullClassicOnline.Server.Hubs;

public class LobbyHub : Hub
{
    public async Task SendMessage(string user, string message, string group)
    {
        await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        Clients.Group(groupName).SendAsync("ReceiveLobbyName", groupName);
    }

    public Task LeaveGroup(string groupName)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}