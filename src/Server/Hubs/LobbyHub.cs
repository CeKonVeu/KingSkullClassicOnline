using System.Collections.Concurrent;
using KingSkullClassicOnline.Engine;
using KingSkullClassicOnline.Engine.Game;
using Microsoft.AspNetCore.SignalR;

namespace KingSkullClassicOnline.Server.Hubs;

/// <summary>
///     Hub qui gère les différents lobby de jeu
/// </summary>
public class LobbyHub : Hub
{
    private static readonly ConcurrentDictionary<string, Controller> groups = new();

    /// <summary>
    ///     signale aux joueurs que la partie se lance
    /// </summary>
    /// <param name="lobbyName"></param>
    public async Task StartGame(string lobbyName)
    {
        //await Clients.Group(lobbyName).SendAsync("StartGame");
        await Clients.Group(lobbyName).SendAsync("StartGame");
    }

    /// <summary>
    ///     Attend que les joueurs soient prets
    /// </summary>
    /// <param name="lobbyName">nom du groupe auquel appartient le joueur</param>
    public async Task ReadyGame(string lobbyName, string playerName)
    {
        groups[lobbyName].SetConnectionId(playerName, Context.ConnectionId);
        if (++groups[lobbyName].areReady == groups[lobbyName].Players.Count)
            Game(lobbyName);
    }

    /// <summary>
    ///     boucle de jeu
    /// </summary>
    /// <param name="lobbyName">lobby que gère la boucle de jeu</param>
    private async Task Game(string lobbyName)
    {
        var controller = groups[lobbyName];
        //while (controller.Turn <= Config.TurnNumber)
        //{
        controller.CurrentRound = new Round(controller);
        controller.Turn = 4;
        controller.CurrentRound.DealCards();
        foreach (var player in groups[lobbyName].Players)
        {
            var res = string.Join(",", player.Hand.Select(card => card.Name));
            await Clients.Client(groups[lobbyName].GetConnectionId(player.Name))
                .SendAsync("ReceiveStartingHand", res);
            //Clients.Group(lobbyName).SendAsync("ReceiveStartingHand", res, groups[lobbyName].GetConnectionId(player.Name));
        }

        Clients.Group(lobbyName).SendAsync("AskVote");

        // TODO gestion du pli
        controller.Turn++;
        //}
    }

    /// <summary>
    ///     TODO : A supprimer ou faire un chat
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <param name="group"></param>
    public async Task SendMessage(string user, string message, string group)
    {
        await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    /// <summary>
    ///     Permet de créer un lobby de jeu
    /// </summary>
    /// <param name="lobbyName">Nom du lobby à créer</param>
    /// <param name="playerName">Nom du créateur du lobby</param>
    public async Task CreateGroup(string lobbyName, string playerName)
    {
        if (groups.ContainsKey(lobbyName))
            return;
        await Groups.AddToGroupAsync(Context.ConnectionId, lobbyName);
        groups.TryAdd(lobbyName, new Controller());
        groups[lobbyName].AddPlayer(new Player(playerName, groups[lobbyName]), Context.ConnectionId);
        Clients.Caller.SendAsync("ReceiveLobbyName", lobbyName);
    }

    /// <summary>
    ///     Rejoindre un lobby de jeu.
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
        groups[lobbyName].AddPlayer(new Player(playerName, groups[lobbyName]), Context.ConnectionId);
    }

    /// <summary>
    ///     Permet de savoir si un lobby existe ou non.
    /// </summary>
    /// <param name="lobbyName">Nom du lobby</param>
    /// <returns>
    ///     Le nom du lobby si ce dernier existe via la méthode DoesLobbyExist de l'utilisateur, string vide dans le cas
    ///     contraire
    /// </returns>
    public async Task DoesLobbyExist(string lobbyName)
    {
        var result = "";
        if (groups.ContainsKey(lobbyName))
            result = lobbyName;
        Clients.Caller.SendAsync("DoesLobbyExist", result);
    }

    /// <summary>
    ///     Quitte le lobby de jeu
    /// </summary>
    /// <param name="lobbyName">Nom du lobby à quitter</param>
    /// <param name="playerName">Nom du joueur</param>
    public void LeaveGroup(string lobbyName, string playerName)
    {
        if (groups.ContainsKey(lobbyName)) groups[lobbyName].RemovePlayer(playerName);
    }

    public async Task SendVote(int vote, string lobbyName, string playerName)
    {
    }
}