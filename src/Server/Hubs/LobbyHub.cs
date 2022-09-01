namespace KingSkullClassicOnline.Server.Hubs;

using System.Collections.Concurrent;
using Engine;
using Microsoft.AspNetCore.SignalR;

/// <summary>
///     Hub qui gère les différents lobby de jeu
/// </summary>
public class LobbyHub : Hub
{
    private static readonly ConcurrentDictionary<string, Controller> Controllers = new();

    /// <summary>
    ///     Permet de créer un lobby de jeu
    /// </summary>
    /// <param name="playerName">Nom du créateur du lobby</param>
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

    private static string CreateRoomName()
    {
        return Guid.NewGuid().ToString("N");
    }

    /// <summary>
    ///     Rejoindre un lobby de jeu.
    /// </summary>
    /// <param name="roomName">Nom du lobby à rejoindre</param>
    /// <param name="playerName">Nom du joueur qui rejoint le lobby</param>
    /// <returns>Renvoi le nom du lobby s'il existe en appelant la méthode ReceiveLobbyName, sinon ne renvoi rien</returns>
    public async Task JoinRoom(string roomName, string playerName)
    {
        if (!Controllers.ContainsKey(roomName))
            throw new Exception("Room doesn't exist");
        //TODO changer le 6 par le nombre de joueur max
        if (Controllers[roomName].Players.Count >= Config.MaxPlayer)
            throw new Exception("Room is full");

        var task = Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        Controllers[roomName].AddPlayer(new Player(Context.ConnectionId, playerName));
        await task;
        await SendRoomJoined(roomName);
        Console.WriteLine($"Player {playerName} joined room {roomName}");
    }


    private Task SendRoomJoined(string roomName)
    {
        return Clients.Group(roomName).SendAsync("RoomJoined",
            roomName,
            Controllers[roomName].Players.Select(p => p.Name));
    }
}