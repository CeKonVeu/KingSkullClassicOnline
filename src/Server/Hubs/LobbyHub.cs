namespace KingSkullClassicOnline.Server.Hubs;

using System.Collections.Concurrent;
using Engine;
using Microsoft.AspNetCore.SignalR;

/// <summary>
///     Hub permettant de gérer la communication entre les clients et le serveur.
/// </summary>
public class LobbyHub : Hub
{
    private static readonly ConcurrentDictionary<string, Controller> Controllers = new();
    private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new();


    /// <summary>
    ///     Créé une salle de jeu.
    /// </summary>
    /// <param name="playerName">le créateur de la salle</param>
    /// <exception cref="Exception"></exception>
    public async Task CreateRoom(string playerName)
    {
        var roomName = CreateRoomName();
        if (Controllers.ContainsKey(roomName))
            //TODO à changer
            throw new Exception("Room already exists");

        if (!Controllers.TryAdd(roomName, new Controller()))
            throw new Exception("Cannot add room");


        await JoinRoom(roomName, playerName);
        Console.WriteLine($"Room {roomName} created");
    }

    /// <summary>
    ///     Obtient un nom de salle unique.
    /// </summary>
    /// <returns>le nom</returns>
    private static string CreateRoomName()
    {
        return Guid.NewGuid().ToString("N");
    }

    /// <summary>
    ///     Rejoint la salle de nom donné.
    /// </summary>
    /// <param name="roomName">la salle à rejoindre</param>
    /// <param name="playerName">le nom du joueur rejoignant la salle</param>
    /// <exception cref="Exception">si la salle n'existe pas ou qu'elle est pleine</exception>
    public async Task JoinRoom(string roomName, string playerName)
    {
        if (!Controllers.ContainsKey(roomName))
            throw new Exception("Room doesn't exist");
        //TODO changer le 6 par le nombre de joueur max
        if (Controllers[roomName].Players.Count >= Config.MaxPlayer)
            throw new Exception("Room is full");

        var task = Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        Controllers[roomName].AddPlayer(new Player(Context.ConnectionId, playerName));
        ConnectedUsers.TryAdd(Context.ConnectionId, roomName);
        await task;
        await SendRoomChanged(roomName);
        Console.WriteLine($"Player {playerName} joined room {roomName}");
    }

    /// <summary>
    ///     Quitte la salle donnée (et la supprime si nécessaire). Notifie les autres joueurs si nécessaire.
    /// </summary>
    /// <param name="roomName">la salle à quitter</param>
    private async Task LeaveRoom(string roomName)
    {
        var task = Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        Controllers[roomName].RemovePlayer(Context.ConnectionId);

        if (Controllers[roomName].Players.Count == 0)
        {
            Controllers.TryRemove(roomName, out _);
            await task;
            Console.WriteLine($"Room {roomName} deleted");
        }
        else
        {
            await task;
            await SendRoomChanged(roomName);
            Console.WriteLine($"Player {Context.ConnectionId} left room {roomName}");
        }
    }

    /// <summary>
    ///     Met à jour les dictionnaires lors d'une déconnexion.
    /// </summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        ConnectedUsers.Remove(Context.ConnectionId, out var rooName);
        if (rooName == null)
            throw new Exception("Player not found");
        await LeaveRoom(rooName);
    }


    /// <summary>
    ///     Notifie les joueurs d'une salle d'un changement de joueur.
    /// </summary>
    /// <param name="roomName">la salle</param>
    /// <returns>la tâche</returns>
    private Task SendRoomChanged(string roomName)
    {
        return Clients.Group(roomName).SendAsync("RoomChanged",
            roomName,
            Controllers[roomName].Players.Select(p => p.Name));
    }

    public async Task SendVote(int vote)
    {
        var roomName = ConnectedUsers[Context.ConnectionId];
        var controller = Controllers[roomName];

        if (!controller.SetVote(Context.ConnectionId, vote))
            return;

        Console.WriteLine($"Player {Context.ConnectionId} must play");
        var player = controller.GetCurrentPlayer();
        await Clients.Client(player).SendAsync("MustPlay");
    }

    public async Task StartGame(string roomName)
    {
        var controller = Controllers[roomName];
        controller.NewTurn();
        var tasks = new Task[controller.Players.Count];
        for (var i = 0; i < controller.Players.Count; ++i)
        {
            var player = controller.Players[i];
            var hand = player.Hand.Select(card => card.Name);

            tasks[i] = Clients.Client(player.Id).SendAsync("HandChanged", hand);
        }

        await Task.WhenAll(tasks);
        await Clients.Group(roomName).SendAsync("VoteAsked", controller.Turn);
    }

    public async Task Vote(string rooName, int vote)
    {
        var currentRound = Controllers[rooName].CurrentRound;
        if (currentRound == null)
            throw new Exception("Controller not found");

        currentRound.AddVote(Context.ConnectionId, vote);
    }
}