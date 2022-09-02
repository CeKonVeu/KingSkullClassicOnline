namespace KingSkullClassicOnline.Engine.Game;

using Card;

/// <summary>
///     Gère le déroulement d'un pli.
/// </summary>
public class Fold
{
    /// <summary>
    ///     Construit un pli.
    /// </summary>
    public Fold()
    {
        CardsPlayed = new LinkedList<Play>();
        TurnColor = Colors.None;
    }

    public ICollection<Play> CardsPlayed { get; }

    public bool HasSkullKing { get; private set; }

    public Colors TurnColor { get; private set; }

    /// <summary>
    ///     Compte le nombre de pirates dans le pli.
    /// </summary>
    /// <returns>le nombre de pirates</returns>
    public int CountPirates()
    {
        return CardsPlayed.Count(card => card.isPirate());
    }

    /// <summary>
    ///     Détermine le gagnant du pli.
    /// </summary>
    /// <returns>le gagnant</returns>
    /// <exception cref="Exception">Si aucun gagné n'est trouvé</exception>
    public Play GetWinner()
    {
        HasSkullKing = false;
        Play? winningPlay = null;
        Play? mermaidPlay = null;

        foreach (var play in CardsPlayed)
        {
            if (play.IsMermaid())
                mermaidPlay = play;
            else if (play.IsSkullKing())
                HasSkullKing = true;

            // La première carte jouée est la gagnante par défaut
            if (winningPlay == null)
            {
                winningPlay = play;
                continue;
            }

            // La sirène bolosse le Skull King
            if (HasSkullKing && mermaidPlay != null)
                return mermaidPlay;

            // Gère toutes les cartes spéciales
            if (play.IsSpecial())
            {
                if (play.IsStrongerThan(winningPlay)) winningPlay = play;
                continue;
            }

            // Cartes normales de même couleur que la carte gagnante
            if (play.HasSameColorAs(winningPlay))
            {
                if (play.IsStrongerThan(winningPlay)) winningPlay = play;
                continue;
            }

            if (!play.IsBlack()) continue;

            if (winningPlay.IsSpecial())
            {
                if (winningPlay.IsEscape()) winningPlay = play;

                continue;
            }

            winningPlay = play;
        }

        return winningPlay ?? throw new Exception("On sait pas coder.");
    }

    /// <summary>
    ///     Joue une carte dans le pli.
    /// </summary>
    /// <param name="player">le joueur qui joue</param>
    /// <param name="card">la carte jouée</param>
    public void PlayCard(Player player, Card card)
    {
        var play = new Play(player, card);

        if (!play.IsSpecial() && TurnColor == Colors.None) TurnColor = card.Color;

        CardsPlayed.Add(play);
    }
}