using KingSkullClassicOnline.Engine;
using KingSkullClassicOnline.Engine.Card;

/// <summary>
///     Gère une carte jouée ainsi que le joueur l'ayant fait
/// </summary>
public class Play
{
    public Play(Player player, Card card)
    {
        Player = player;
        Card = card;
    }

    public Player Player { get; }

    public Card Card { get; }
}