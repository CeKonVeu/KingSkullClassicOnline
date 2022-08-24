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
    /// nom représentant la carte
    /// </summary>
    private string Name { get; }

    /// <summary>
    /// constructeur
    /// </summary>
    /// <param name="value">valeur de la carte</param>
    /// <param name="name"></param>
    public Card(int value, string name)
    {
        Name = name;
        Value = value;
    }
}