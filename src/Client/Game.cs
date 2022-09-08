using KingSkullClassicOnline.Engine;
using KingSkullClassicOnline.Engine.Cards;
using KingSkullClassicOnline.Engine.Game;

namespace KingSkullClassicOnline.Client;

public class Game
{
    public Player? StartingPlayer = null;
    public Player? Winner = null;


    public Game()
    {
        CurrentFold = 1;
        Turn = 1;
        Players = new List<Player>();
    }

    public int CurrentFold { get; set; }
    public List<Player> Players { get; }

    public int Turn { get; set; }

    public void ClearPlayedCards()
    {
        foreach (var player in Players) player.PlayedCard = null;
    }

    public int GetStartingPlayerIndex()
    {
        return StartingPlayer == null ? 0 : Players.FindIndex(p => p == StartingPlayer);
    }

    public Player Player(string id)
    {
        Player(id, out var p);
        return p ?? throw new Exception("Player not found");
    }

    public bool Player(string id, out Player? player)
    {
        player = Players.SingleOrDefault(p => p.Id == id);
        return player != null;
    }
}

public class Player
{
    public Player(PlayerData data)
    {
        Id = data.Id;
        Name = data.Name;
        Scores = new PlayerScore[Config.RoundsPerGame];
        for (var i = 0; i < Scores.Length; i++) Scores[i] = new PlayerScore();
    }

    public List<Card>? Hand { get; set; } = null;

    public string Id { get; }
    public string Name { get; }

    public Card? PlayedCard { get; set; }
    public PlayerScore[] Scores { get; set; }
}

public class PlayerScore
{
    public int Actual { get; set; }

    public int Total { get; set; } = 0;
    public int Voted { get; set; }
}