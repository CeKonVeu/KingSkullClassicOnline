namespace KingSkullClassicOnline.Engine.Game;

/// <summary>
///     gère une manche de jeu
/// </summary>
public class Round
{
    private static readonly Random Random = new();
    private readonly Controller _controller;

    /// <summary>
    ///     constructeur
    /// </summary>
    /// <param name="numPlayers">nombre de joueurs dans la partie</param>
    /// <param name="turn">tour du round</param>
    public Round(Controller controller)
    {
        CurrentPlayer = 0;
        _controller = controller;
        Votes = new int[controller.Players.Count];
        Plis = new Fold[controller.Turn];
    }

    public int[] Votes { get; }

    public Fold[] Plis { get; }

    public int CurrentPlayer { get; set; }

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
        for (var index = 0; index < _controller.Players.Count; index++)
        {
            var player = _controller.Players[index];
            Votes[index] = player.CurrentVote;
        }

        var lastWinner = 0;

        for (var i = 0; i < _controller.Turn; ++i)
        {
            Plis[i] = new Fold();
            for (var index = 0; index < _controller.Players.Count; index++)
            {
                var p = _controller.Players[(index + lastWinner) % _controller.Players.Count];
                CurrentPlayer++;
                var indexCard = p.PlayCard(Plis[i].TurnColor);
                var cardPlayed = p.Hand[indexCard];
                p.Hand.RemoveAt(indexCard);
                Plis[i].PlayCard(p, cardPlayed);
            }

            lastWinner = _controller.Players.IndexOf(Plis[i].GetWinner().Player);
        }

        CurrentPlayer = 0;
        for (var index = 0; index < _controller.Players.Count; index++)
        {
            var p = _controller.Players[index];
            ScoreCalculator.UpdateScore(p, Plis, Votes[index], _controller.Turn);
        }

        ChangePlayerOrder();
    }

    /// <summary>
    ///     Change l'ordre des joueurs
    /// </summary>
    private void ChangePlayerOrder()
    {
        var temp = _controller.Players.First();
        _controller.Players.RemoveAt(0);
        _controller.Players.Add(temp);
    }
}