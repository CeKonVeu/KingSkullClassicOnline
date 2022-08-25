﻿using Microsoft.AspNetCore.SignalR;
using MudBlazor;

namespace KingSkullClassicOnline.Server.Hubs;

public class LobbyHub : Hub
{
    private static Dictionary<String,int> groupsNumber = new Dictionary<String,int>();

    public async Task SendMessage(string user, string message, string group)
    {
        await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    public async Task CreateGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        if (groupsNumber.ContainsKey(groupName))
            return;
        groupsNumber.Add(groupName, 1);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        Clients.Group(groupName).SendAsync("ReceiveLobbyName", groupName);
    }
    public async Task JoinGroup(string groupName)
    {
        //cannot join if there is already 6 players in the group
        if (!groupsNumber.ContainsKey(groupName) || groupsNumber[groupName] >= 6)
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