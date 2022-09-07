namespace KingSkullClassicOnline.Client;

using Engine.Game;
using Shared.Components;

public class Game
{
    public Game()
    {
        CurrentFold = 0;
        Turn = 1;
        Players = new Dictionary<string, Player>();
    }

    public int CurrentFold { get; set; }
    public Dictionary<string, Player> Players { get; }
    public int Turn { get; set; }
}

public class Player
{
    public PlayerData Data { get; set; }
    public List<HandGame.Card>? Hand { get; set; } = null;
    public PlayerCard? PlayedCard { get; set; } = null;
    public List<PlayerScore> Scores { get; set; }
}

public class PlayerScore
{
    public int Actual { get; set; } = 0;
    public int Total { get; set; } = 0;
    public int Voted { get; set; } = 0;
}

public class PlayerCard
{
    public bool IsWinning { get; set; } = false;
    public string Name { get; set; }
}