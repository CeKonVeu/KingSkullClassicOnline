using Microsoft.AspNetCore.SignalR;
using MudBlazor;
using KingSkullClassicOnline.Engine;

namespace KingSkullClassicOnline.Server.Hubs;

/// <summary>
/// Hub qui gère les différents lobby de jeu
/// </summary>
public class LobbyHub : Hub
{
    private static Dictionary<String,Controller> groups = new Dictionary<String,Controller>();

    /// <summary>
    /// TODO : A supprimer ou faire un chat
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <param name="group"></param>
    public async Task SendMessage(string user, string message, string group)
    {
        await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    /// <summary>
    /// Permet de créer un lobby de jeu
    /// </summary>
    /// <param name="lobbyName">Nom du lobby à créer</param>
    /// <param name="playerName">Nom du créateur du lobby</param>
    public async Task CreateGroup(string lobbyName, string playerName)
    {
        if (groups.ContainsKey(lobbyName))
            return;
        await Groups.AddToGroupAsync(Context.ConnectionId, lobbyName);
        groups.Add(lobbyName, new Controller());
        Clients.Caller.SendAsync("ReceiveLobbyName", lobbyName);
        Player player = new Player(playerName, groups[lobbyName]);
    }
    
    /// <summary>
    /// Rejoindre un lobby de jeu.
    /// </summary>
    /// <param name="lobbyName">Nom du lobby à rejoindre</param>
    /// <param name="playerName">Nom du joueur qui rejoint le lobby</param>
    /// <returns>Renvoi le nom du lobby s'il existe en appelant la méthode ReceiveLobbyName, sinon ne renvoi rien</returns>
    public async Task JoinGroup(string lobbyName, string playerName)
    {
        if (!groups.ContainsKey(lobbyName) || groups[lobbyName].Players.Count >= 6)
            return;

        await Groups.AddToGroupAsync(Context.ConnectionId, lobbyName);
        Clients.Caller.SendAsync("ReceiveLobbyName", lobbyName);
        Player player = new Player(playerName, groups[lobbyName]);
    }

    /// <summary>
    /// Permet de savoir si un lobby existe ou non.
    /// </summary>
    /// <param name="lobbyName">Nom du lobby</param>
    /// <returns>Le nom du lobby si ce dernier existe via la méthode DoesLobbyExist de l'utilisateur, string vide dans le cas contraire</returns>
    public async Task DoesLobbyExist(string lobbyName)
    {
        string result = "";
        if (groups.ContainsKey(lobbyName))
            result = lobbyName;
        Clients.Caller.SendAsync("DoesLobbyExist",result);
    }
    
    /// <summary>
    /// Quitte le lobby de jeu
    /// </summary>
    /// <param name="lobbyName">Nom du lobby à quitter</param>
    /// <param name="playerName">Nom du joueur</param>
    public Task LeaveGroup(string lobbyName, string playerName)
    {
        if (groups.ContainsKey(lobbyName))
        {
            groups[lobbyName].RemovePlayer(playerName);
            //find the playerName in the group and remove it
            
            if(groups[lobbyName].Players.Count <= 0)
            {
                groups.Remove(lobbyName);
            }
        }
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyName);
    }
}