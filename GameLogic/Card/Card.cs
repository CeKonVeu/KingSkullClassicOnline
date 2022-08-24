namespace GameLogic.Card;

/// <summary>
/// Carte de jeu
/// </summary>
public abstract class Card
{
    /// <summary>
    /// valeur de la carte
    /// </summary>
    private int Value { get; }

    /// <summary>
    /// constructeur
    /// </summary>
    /// <param name="value">valeur de la carte</param>
    public Card(int value)
    {
        Value = value;
    }
}