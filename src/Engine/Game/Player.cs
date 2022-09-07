namespace KingSkullClassicOnline.Engine.Game;

using Cards;

public class Player
{
    private readonly Vote?[] _votes;

    public Player(string id, string name) : this(new PlayerData(id, name))
    {
    }

    public Player(PlayerData data)
    {
        Data = data;
        Hand = new List<Card>();
        _votes = new Vote[Config.RoundsPerGame];
    }

    public PlayerData Data { get; }

    public List<Card> Hand { get; internal set; }

    private bool CheckIfCardRespectsRule(Color turnColor)
    {
        return true;
        // var temp = false;
        // var playableCards = new List<int>();
        // var hasCardOfColor = false;
        //
        // foreach (var card in Hand)
        //     if (card.Color == TurnColor)
        //     {
        //         hasCardOfColor = true;
        //         break;
        //     }
        //
        // if (!hasCardOfColor)
        //     for (var i = 0; i < Hand.Count; ++i)
        //         playableCards.Add(i);
        // else
        //     for (var i = 0; i < Hand.Count; i++)
        //         if (hasCardOfColor)
        //             if (Hand[i].Color == Colors.None || Hand[i].Color == TurnColor)
        //                 playableCards.Add(i);
        //
        // return playableCards.Contains(_selectedCard);
    }

    public Vote GetVote(int turn)
    {
        return _votes[turn - 1] ?? throw new InvalidOperationException();
    }

    public int PlayCard(Color turnColor)
    {
        return 1;
    }

    public void SetVote(int turn, int voted)
    {
        _votes[turn - 1] = new Vote(voted);
    }

    public void SetTotal(int turn,int total)
    {
        int? score = 0;
        if(turn > 1)
            score = _votes[turn - 2]?.Total;
        _votes[turn - 1]!.Total = total + score;
    }
    
    public void AddActual(int turn)
    {
        _votes[turn - 1]!.Actual+=1;
    }
}