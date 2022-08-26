using Microsoft.AspNetCore.SignalR;
using MudBlazor;

namespace KingSkullClassicOnline.Server.Hubs;

public class LobbyHub : Hub
{
    private static Dictionary<String,int> groupsNumber = new Dictionary<String,int>();

    public async Task SendMessage(string user, string message, string group)
    {
        await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinGroup(string groupName)
    {
        //cannot join if there is already 6 players in the group
        if (!groupsNumber.ContainsKey(groupName))
        {
            groupsNumber.Add(groupName,0);
        } else if (groupsNumber[groupName] >= 6)
            return;

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        Clients.Group(groupName).SendAsync("ReceiveLobbyName", groupName);
        ++groupsNumber[groupName];
    }

    public Task LeaveGroup(string groupName)
    {
        --groupsNumber[groupName];
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}