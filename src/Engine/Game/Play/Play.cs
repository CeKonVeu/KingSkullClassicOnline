namespace KingSkullClassicOnline.Engine.Game;

using Cards;

/// <summary>
///     Fait le lien entre une carte et le joueur qui l'a jouée.
/// </summary>
public partial record Play(Player Player, Card Card);