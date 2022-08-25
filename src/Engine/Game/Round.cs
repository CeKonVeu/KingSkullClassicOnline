namespace KingSkullClassicOnline.Engine.Game;

/// <summary>
///     gère une manche de jeu
/// </summary>
public class Round
{
    private static readonly Random Random = new();
    private readonly Controller _controller;
    private readonly Fold[] _plis;
    private int[] _votes;

    /// <summary>
    ///     constructeur
    /// </summary>
    /// <param name="numPlayers">nombre de joueurs dans la partie</param>
    /// <param name="turn">tour du round</param>
    public Round(int numPlayers, int turn, Controller controller)
    {
        CurrentPlayer = 0;
        _controller = controller;
        _votes = new int[numPlayers];
        _plis = new Fold[turn];
    }

    public int CurrentPlayer { get; }

    /// <summary>
    ///     ajoute les votes aux joueurs
    /// </summary>
    /// <param name="votes"></param>
    public void addVotes(int[] votes)
    {
        _votes = votes;
    }

    /// <summary>
    ///     mets à jour le score de chaque joueurs selon les résultats des plis
    /// </summary>
    public void UpdateScore()
    {
        for (var i = 0; i < _controller.Players.Count; i++)
        {
            int bonusPoints = 0, wonFold = 0, score;

            foreach (var fold in _plis)
                if (fold.GetWinner().Player == _controller.Players[i])
                {
                    wonFold++;
                    if (fold.GetWinner().Card == Config.MermaidValue && fold.HasSkullKing)
                        bonusPoints += Config.BonusMermaid;
                    else if (fold.GetWinner().Card == Config.SkullKingValue)
                        bonusPoints += fold.GetNumberPirate() * Config.BonusSkullKing;
                }

            if (wonFold == _votes[i])
                if (_votes[i] == 0)
                    score = bonusPoints + Config.Score0 * _controller.Turn;
                else
                    score = bonusPoints + Config.ScoreVoted * _votes[i];
            else if (_votes[i] == 0)
                score = -(Config.Score0 * _controller.Turn);
            else
                score = Config.ScoreBadVote * Math.Abs(_votes[i] - wonFold);

            _controller.Players[i].AddScore(_votes[i], score);
        }
    }

    /// <summary>
    ///     Mélange un array utilisant le "fisher yates shuffle"
    /// </summary>
    /// <param name="array">l'array à mélanger</param>
    /// <typeparam name="T">type du contenu de l'array</typeparam>
    /// <returns>l'array mélangé</returns>
    public static T[] Shuffle<T>(T[] array)
    {
        var n = array.Length;
        for (var i = 0; i < n - 1; i++)
        {
            var r = i + Random.Next(n - i);
            (array[r], array[i]) = (array[i], array[r]);
        }

        return array;
    }

    /// <summary>
    ///     fait une copie du deck et distribue les cartes aux joueurs
    /// </summary>
    public void DealCards()
    {
        var temp = new List<Card.Card>();
        temp.AddRange(Shuffle(_controller.Deck.ToArray()));

        for (var index = 0; index < _controller.Players.Count; index++)
            _controller.Players[index].AddCards(temp.GetRange(0 + index * _controller.Turn, _controller.Turn));
    }

    /// <summary>
    ///     gère le tour actuel
    /// </summary>
    public void Play()
    {
        DealCards();
        for (var i = 0; i < _controller.Turn; ++i)
        {
            _plis[i] = new Fold();
            foreach (var p in _controller.Players)
            {
                var cardPlayed = p.Hand[p.PlayCard(_plis[i].TurnColor)];
                _plis[i].PlayCard(p, cardPlayed);
            }
        }

        UpdateScore();
    }
}