namespace GameLogic;

/// <summary>
///     gère le déroulement d'un pli
/// </summary>
public class Fold
{
    private readonly List<Play> _cardsPlayed;

    /// <summary>
    ///     constructeur
    /// </summary>
    public Fold()
    {
        _cardsPlayed = new List<Play>();
    }

    public bool HasSkullKing { get; private set; }

    /// <summary>
    ///     rajoute une carte aux cartes jouées
    /// </summary>
    /// <param name="p">joueur jouant la carte</param>
    /// <param name="c">carte jouée</param>
    public void PlayCard(Player p, Card.Card c)
    {
        _cardsPlayed.Add(new Play(p, c));
    }

    /// <summary>
    ///     détermine le gagnant du pli en fonction des cartes jouées
    /// </summary>
    /// <returns>le gagnant du pli</returns>
    public Play GetWinner()
    {
        Play? winningPlay = null, sirenPlay = null;
        HasSkullKing = false;
        foreach (var play in _cardsPlayed)
        {
            if (play.Card == Config.MermaidValue)
                sirenPlay = play;
            else if (play.Card == Config.SkullKingValue)
                HasSkullKing = true;

            if (HasSkullKing && sirenPlay != null)
                winningPlay = sirenPlay;

            else if (winningPlay == null || winningPlay.Card < play.Card) winningPlay = play;
        }

        return winningPlay;
    }

    /// <summary>
    ///     retourne le nombre de pirate joués dans le pli
    /// </summary>
    /// <returns>le nombre de pirates joués</returns>
    public int GetNumberPirate()
    {
        var nbPirates = 0;
        foreach (var card in _cardsPlayed)
            if (card.Card == Config.PirateValue)
                nbPirates++;
        return nbPirates;
    }
}