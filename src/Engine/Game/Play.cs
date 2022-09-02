namespace KingSkullClassicOnline.Engine.Game;

using Card;

/// <summary>
///     Gère une carte jouée ainsi que le joueur l'ayant fait
/// </summary>
public record Play(Player Player, Card Card);