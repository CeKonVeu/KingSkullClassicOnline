namespace GameLogic.Card;

/// <summary>
/// gère les cartes numérotées
/// </summary>
public class NumberedCard : Card
{
    /// <summary>
    /// couleur de la carte
    /// </summary>
    private Colors Color { get; }

    /// <summary>
    /// constructeur
    /// </summary>
    /// <param name="value">valeur de la carte</param>
    /// <param name="name">nom de la carte</param>
    /// <param name="color">couleur de la carte</param>
    public NumberedCard(int value, string name,Colors color) : base(value, name)
    {
        Color = color;
    }
}