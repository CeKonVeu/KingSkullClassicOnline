namespace KingSkullClassicOnline.Server.Views;

using Engine;
using Engine.Cards;
using Engine.Game;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Shared;

public class SignalRView : IView
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly LinkedList<PlayerData> _players;
    private string _group = null!;

    public SignalRView(IHubContext<GameHub> hubContext)
    {
        _hubContext = hubContext;
        _players = new LinkedList<PlayerData>();
    }

    public async Task RoomCreated(string roomName, PlayerData player)
    {
        Console.WriteLine($"Room {roomName} created by {player.Name}");
        _group = roomName;
        await PlayerJoined(player);
    }

    public async Task CardPlayed(PlayerData player, string card, string winnerName)
    {
        Console.WriteLine($"{player.Name} played {card}");
        await _hubContext.Clients.Group(_group).SendAsync("CardPlayed", player.Id, card,  winnerName);
    }

    public async Task GameEnded(string[] scores, string winner)
    {
        throw new NotImplementedException();
    }

    public async Task GameStarted()
    {
        Console.WriteLine("Game has started");
        await _hubContext.Clients.Group(_group).SendAsync(Events.GameStarted);

        throw new NotImplementedException();
    }

    public async Task HandReceived(PlayerData player, List<Card> cards)
    {
        Console.WriteLine($"Player {player.Name} received: {string.Join(", ", cards.Select(c => c.Name))}");
        await _hubContext.Clients.Client(player.Id).SendAsync(Events.HandChanged, cards.Select(c => c.Name));
    }

    public async Task MustPlay(PlayerData player, IEnumerable<Card> availableCards)
    {
        var cards = availableCards.Select(c => c.Name);
        Console.WriteLine($"Player {player.Name} must play with {string.Join(", ", cards)}");
        await _hubContext.Clients.Client(player.Id).SendAsync(Events.MustPlay, cards);
    }

    public async Task PlayerJoined(PlayerData player)
    {
        Console.WriteLine($"Player {player.Name} joined room {_group}");
        await AddToGroup(player);
        await SendPlayers();
    }

    public async Task PlayerLeft(PlayerData player)
    {
        Console.WriteLine($"Player {player.Name} left");
        await RemoveFromGroup(player);
        await SendPlayers();
    }

    public async Task RoundEnded(int[] scores)
    {
        await _hubContext.Clients.Group(_group).SendAsync(Events.RoundEnded, scores);
    }

    public async Task NotifyError(PlayerData player, string message)
    {
        //TODO créer l'event
        await _hubContext.Clients.Client(player.Id).SendAsync(Events.OnError, message);
    }

    public async Task MustVote(int min, int max)
    {
        Console.WriteLine($"Must vote between {min} and {max}");
        await _hubContext.Clients.Group(_group).SendAsync(Events.VoteAsked, min, max);
    }

    private async Task AddToGroup(PlayerData player)
    {
        var task = _hubContext.Groups.AddToGroupAsync(player.Id, _group);
        _players.AddLast(player);
        await task;
    }

    private async Task RemoveFromGroup(PlayerData player)
    {
        var task = _hubContext.Groups.RemoveFromGroupAsync(player.Id, _group);
        _players.Remove(player);
        await task;
    }

    private async Task SendPlayers()
    {
        await _hubContext.Clients.Group(_group).SendAsync(Events.RoomChanged, _group, _players.Select(p => p.Name));
    }

    private async Task SendToGroup(string method, params object?[] args)
    {
        Console.WriteLine($"Sending {method} to {_group}");
        await _hubContext.Clients.Group(_group).SendAsync(method, args);
    }

    private async Task SendToPlayer(PlayerData player, string method, params object[] args)
    {
        await _hubContext.Clients.Client(player.Id).SendAsync(method, args);
    }
}