namespace GameLogic.Card;

/// <summary>
/// gère les cartes spéciales (sirènes, pirates, skull king)
/// </summary>
public class SpecialCard : Card
{
    /// <summary>
    /// constructeur
    /// </summary>
    /// <param name="value">valeur de la carte</param>
    public SpecialCard(int value) : base(value)
    {
    }
}