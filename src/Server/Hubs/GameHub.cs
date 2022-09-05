using KingSkullClassicOnline.Shared;

namespace KingSkullClassicOnline.Server.Hubs;

using System.Collections.Concurrent;
using Engine;
using Microsoft.AspNetCore.SignalR;
using Views;

/// <summary>
///     Hub permettant de gérer la communication entre les clients et le serveur.
/// </summary>
public class GameHub : Hub
{
    private static readonly ConcurrentDictionary<string, Controller> Controllers = new();
    private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new();
    private readonly IServiceProvider _serviceProvider;

    public GameHub(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     Créé une salle de jeu.
    /// </summary>
    /// <param name="playerName">le créateur de la salle</param>
    /// <exception cref="Exception"></exception>
    public async Task CreateRoom(string playerName)
    {
        var roomName = CreateRoomName();

        var inserted = Controllers.TryAdd(roomName,
            new Controller(_serviceProvider.GetRequiredService<SignalRView>(),
                roomName,
                Context.ConnectionId,
                playerName));

        if (!inserted)
            throw new Exception("Room already exists");

        ConnectedUsers.TryAdd(Context.ConnectionId, roomName);
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
        if (!Controllers.TryGetValue(roomName, out var controller))
            throw new Exception("Room doesn't exist");

        controller.JoinGame(Context.ConnectionId, playerName);

        ConnectedUsers.TryAdd(Context.ConnectionId, roomName);
    }

    /// <summary>
    ///     Met à jour les dictionnaires lors d'une déconnexion.
    /// </summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (!ConnectedUsers.TryRemove(Context.ConnectionId, out var roomName))
            throw new Exception("Player not found");

        var controller = Controllers[roomName];

        if (!controller.LeaveGame(Context.ConnectionId))
        {
            Controllers.Remove(roomName, out _);
            Console.WriteLine($"Room {roomName} deleted");
        }
        else
        {
            Console.WriteLine($"Player {Context.ConnectionId} left room {roomName}");
        }
    }

    public async Task PlayCard(string card)
    {
        var roomName = ConnectedUsers[Context.ConnectionId];
        var controller = Controllers[roomName];

        controller.PlayCard(Context.ConnectionId, card);
    }

    public async Task SendVote(int vote)
    {
        var roomName = ConnectedUsers[Context.ConnectionId];
        Controllers[roomName].SetVote(Context.ConnectionId, vote);
    }

    public async Task StartGame(string roomName)
    {
        Controllers[roomName].StartNextRound();
    }
}