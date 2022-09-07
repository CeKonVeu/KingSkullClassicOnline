namespace KingSkullClassicOnline.Client;

using Engine;
using Engine.Cards;
using Engine.Game;

public class Game
{
    public Player? Winner = null;

    public Game()
    {
        CurrentFold = 1;
        Turn = 1;
        Players = new Dictionary<string, Player>();
    }

    public int CurrentFold { get; set; }
    public Dictionary<string, Player> Players { get; }

    public int Turn { get; set; }

    public void ClearPlayedCards()
    {
        foreach (var (_, player) in Players) player.PlayedCard = null;
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
    public int Actual { get; set; } = 0;
    public int Voted { get; set; } = 0;
}