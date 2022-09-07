namespace KingSkullClassicOnline.Client;

using Engine;
using Engine.Cards;
using Engine.Game;

public class Game
{
    private int _turn;

    public Game()
    {
        CurrentFold = 1;
        Turn = 1;
        Players = new Dictionary<string, Player>();
    }

    public int CurrentFold { get; set; }
    public Dictionary<string, Player> Players { get; }

    public int Turn
    {
        get => _turn;
        set
        {
            _turn = value;
            CurrentFold = 1;
        }
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
    public PlayerCard? PlayedCard { get; set; } = null;
    public PlayerScore[] Scores { get; set; }
}

public class PlayerScore
{
    public int Actual { get; set; } = 0;
    public int Voted { get; set; } = 0;
}

public class PlayerCard
{
    public bool IsWinning { get; set; } = false;
    public string Name { get; set; }
}