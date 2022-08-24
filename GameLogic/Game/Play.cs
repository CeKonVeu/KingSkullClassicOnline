using GameLogic;
using GameLogic.Card;

/// <summary>
/// Gère une carte jouée ainsi que le joueur l'ayant fait
/// </summary>
public class Play
{
    private Player _player;
    private Card _card;

    public Play(Player player, Card card)
    {
        _player = player;
        _card = card;
    }

    public Player Player => _player;

    public Card Card => _card;
}