namespace KingSkullClassicOnline.Engine.Game;

/// <summary>
///     gère le déroulement d'un pli
/// </summary>
public class Fold
{
    /// <summary>
    ///     constructeur
    /// </summary>
    public Fold()
    {
        CardsPlayed = new List<Play>();
        TurnColor = Colors.None;
    }

    public List<Play> CardsPlayed { get; }

    public Colors TurnColor { get; private set; }

    public bool HasSkullKing { get; private set; }

    /// <summary>
    ///     rajoute une carte aux cartes jouées
    /// </summary>
    /// <param name="p">joueur jouant la carte</param>
    /// <param name="c">carte jouée</param>
    public void PlayCard(Player p, Card.Card c)
    {
        if (c < 13 && c > 1 && TurnColor == Colors.None) TurnColor = c.Color;

        CardsPlayed.Add(new Play(p, c));
    }

    /// <summary>
    ///     détermine le gagnant du pli en fonction des cartes jouées
    /// </summary>
    /// <returns>le gagnant du pli</returns>
    public Play GetWinner()
    {
        Play? winningPlay = null, sirenPlay = null;
        HasSkullKing = false;
        foreach (var play in CardsPlayed)
        {
            var isNumbered = play.Card.Value <= 13 && play.Card.Value > 1;

            if (play.Card == Config.MermaidValue)
                sirenPlay = play;
            else if (play.Card == Config.SkullKingValue)
                HasSkullKing = true;

            if (HasSkullKing && sirenPlay != null)
                winningPlay = sirenPlay;

            else if (winningPlay == null ||
                     (isNumbered && TurnColor != Colors.Black && play.Card.Color == Colors.Black) ||
                     (isNumbered && winningPlay.Card < play.Card && play.Card.Color == TurnColor) ||
                     (!isNumbered && winningPlay.Card < play.Card))
                winningPlay = play;
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
        foreach (var card in CardsPlayed)
            if (card.Card == Config.PirateValue)
                nbPirates++;
        return nbPirates;
    }
}