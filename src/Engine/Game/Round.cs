using KingSkullClassicOnline.Engine.Cards;

namespace KingSkullClassicOnline.Engine.Game;

public class Round
{
    private static readonly Random Random = new();
    private readonly List<Card> _deck;
    private readonly Fold[] _folds;
    private readonly IList<Player> _players;
    private readonly int _turn;

    private int _currentFold;
    private int _currentPlayer;
    private int _startingPlayer;

    public Round(int turn, IList<Player> players, IEnumerable<Card> deck)
    {
        _currentPlayer = _startingPlayer = (turn - 1) % players.Count;
        _turn = turn;
        _currentFold = 0;
        _players = players;
        _folds = new Fold[turn];
        for (var i = 0; i < _folds.Length; ++i) _folds[i] = new Fold();
        _deck = Shuffle(deck);
        IsOver = false;
    }

    public Color CurrentColor => _folds[_currentFold].TurnColor;
    public Fold CurrentFold => _folds[_currentFold];
    public bool IsOver { get; private set; }

    public Player NextPlayer => _players[_currentPlayer];

    public void AddVote(Player player, int vote)
    {
        player.SetVote(_turn, vote);
    }

    public bool AreAllVotesIn()
    {
        return _players.All(player => player.GetVote(_turn) != null);
    }

    public void DealCards(Player player)
    {
        player.Hand = _deck.GetRange(0, _turn);
        _deck.RemoveRange(0, _turn);
    }

    public void Play(Player player, Card card)
    {
        CurrentFold.PlayCard(player, card);
        _currentPlayer = NextIndexInCollection(_currentPlayer, _players.Count);
    }

    public void EndRound()
    {
        foreach (var p in _players) ScoreCalculator.UpdateScore(p, _folds, _turn);
    }

    public int EndFold()
    {
        if (_startingPlayer != _currentPlayer) return -1;
        var (winner, _) = CurrentFold.GetWinner();
        winner.AddActual(_turn);
        _currentPlayer = _startingPlayer = _players.IndexOf(winner);
        ++_currentFold;
        if (_currentFold != _turn) return _currentFold;
        IsOver = true;
        return _currentFold;
    }

    public bool IsNewFold()
    {
        return _currentFold == 0;
    }

    private static int NextIndexInCollection(int index, int count)
    {
        return (index + 1) % count;
    }

    public Player[] GetPlayersFromStarting()
    {
        var players = new Player[_players.Count];
        var tmpPlayer = _startingPlayer;
        for (var i = 0; i < _players.Count; i++)
        {
            players[i] = _players[tmpPlayer];
            tmpPlayer = NextIndexInCollection(tmpPlayer, _players.Count);
        }

        return players;
    }

    private static List<T> Shuffle<T>(IEnumerable<T> array)
    {
        var deck = array.ToList();

        var n = deck.Count;
        for (var i = 0; i < n - 1; i++)
        {
            var r = i + Random.Next(n - i);
            (deck[r], deck[i]) = (deck[i], deck[r]);
        }

        return deck;
    }
}