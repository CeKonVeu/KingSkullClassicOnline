using Microsoft.AspNetCore.SignalR.Client;

namespace KingSkullClassicOnline.Client;

public class Data
{
    public string? Name { get; set; }
    public static string? Player { get; set; }
    public static string? LobbyName { get; set; }
    public static HubConnection? HubConnection { get; set; }
}