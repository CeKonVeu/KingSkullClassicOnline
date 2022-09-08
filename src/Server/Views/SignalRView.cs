using KingSkullClassicOnline.Engine;
using KingSkullClassicOnline.Engine.Cards;
using KingSkullClassicOnline.Engine.Game;
using KingSkullClassicOnline.Server.Hubs;
using KingSkullClassicOnline.Shared;
using Microsoft.AspNetCore.SignalR;

namespace KingSkullClassicOnline.Server.Views;

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

    public async Task CardPlayed(PlayerData player, Card card, PlayerData winner)
    {
        Console.WriteLine($"{player.Name} played {card}");
        await _hubContext.Clients.Group(_group).SendAsync(Events.CardPlayed, player, card, winner);
    }

    public async Task GameStarted(IEnumerable<PlayerData> players)
    {
        Console.WriteLine("Game has started");
        await _hubContext.Clients.Group(_group).SendAsync(Events.GameStarted, players);
    }

    public async Task HandReceived(PlayerData player, List<Card> cards)
    {
        Console.WriteLine($"Player {player.Name} received: {string.Join(", ", cards.Select(c => c.Name))}");
        await _hubContext.Clients.Client(player.Id).SendAsync(Events.HandChanged, cards);
    }

    public async Task MustPlay(PlayerData player, List<Card> cards)
    {
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

    public async Task FoldStarted(int fold)
    {
        await _hubContext.Clients.Group(_group).SendAsync(Events.FoldStarted, fold);
    }

    public async Task FoldEnded(int fold, IEnumerable<PlayerVote> votes)
    {
        await _hubContext.Clients.Group(_group).SendAsync(Events.FoldEnded, fold, votes);
    }

    public async Task NotifyError(PlayerData player, string message)
    {
        await _hubContext.Clients.Client(player.Id).SendAsync(Events.OnError, message);
    }

    public async Task MustVote(int min, int max)
    {
        Console.WriteLine($"Must vote between {min} and {max}");
        await _hubContext.Clients.Group(_group).SendAsync(Events.VoteAsked, min, max);
    }

    public async Task RoundEnded(int turn, IEnumerable<PlayerVote> totalScores, PlayerData startingPlayer)
    {
        await _hubContext.Clients.Group(_group).SendAsync(Events.RoundEnded, turn, totalScores, startingPlayer);
    }

    public async Task GameEnded()
    {
        await _hubContext.Clients.Group(_group).SendAsync(Events.GameEnded);
    }

    public async Task RoundStarted(int turn, IEnumerable<PlayerVote> votes)
    {
        await _hubContext.Clients.Group(_group).SendAsync(Events.RoundStarted, turn, votes);
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
}