using KingSkullClassicOnline.Engine.Cards;

namespace KingSkullClassicOnline.Engine.Game;

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
        TurnColor = Color.None;
    }

    public ICollection<Play> CardsPlayed { get; }

    public bool HasSkullKing { get; private set; }

    public Color TurnColor { get; private set; }

    /// <summary>
    ///     Compte le nombre de pirates dans le pli.
    /// </summary>
    /// <returns>le nombre de pirates</returns>
    public int CountPirates()
    {
        return CardsPlayed.Count(card => card.IsPirate());
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

            // La première carte jouée est gagnante par défaut
            if (winningPlay == null)
            {
                winningPlay = play;
                continue;
            }

            // La sirène bolosse le Skull King
            if (HasSkullKing && mermaidPlay != null)
                return mermaidPlay;

            // Pour les cartes spéciales, il suffit de comparer leur valeur
            if (play.IsSpecial())
            {
                if (play.IsStrongerThan(winningPlay))
                    winningPlay = play;
            }
            else
            {
                // Les cartes de couleurs gagnent forcément sur une Escape
                if (winningPlay.IsEscape())
                {
                    winningPlay = play;
                }
                // Si la carte est de la même couleur que la carte gagnante, on compare leur valeur
                // PS : si l'atout est rouge et que quelqu'un joue une carte noire, la couleur gagnante devient le noir
                else if (play.HasSameColorAs(winningPlay))
                {
                    if (play.IsStrongerThan(winningPlay))
                        winningPlay = play;
                }
                // Si la carte n'est pas de la même couleur que la carte gagnante, mais qu'elle est noire,
                // alors elle gagne sur les cartes de couleur
                else if (play.IsBlack() && !winningPlay.IsSpecial())
                {
                    winningPlay = play;
                }
            }
        }
        //TODO WTF POURQUOI çA PASSE DANS LE THROW
        Console.WriteLine(winningPlay.Card.Name);
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

        if (!play.IsSpecial() && TurnColor == Color.None) TurnColor = card.Color;

        player.Hand.Remove(card);
        CardsPlayed.Add(play);
    }
}