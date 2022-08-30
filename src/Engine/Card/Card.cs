namespace KingSkullClassicOnline.Engine.Card;

/// <summary>
///     Carte de jeu
/// </summary>
public abstract class Card
{
    /// <summary>
    ///     constructeur
    /// </summary>
    /// <param name="value">valeur de la carte</param>
    /// <param name="name"></param>
    public Card(int value, string name, Colors color)
    {
        Color = color;
        Name = name;
        Value = value;
    }

    /// <summary>
    ///     couleur de la carte
    /// </summary>
    public Colors Color { get; }

    /// <summary>
    ///     valeur de la carte
    /// </summary>
    public int Value { get; }

    /// <summary>
    ///     nom représentant la carte
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     surcharge de l'opérateur d'égalité
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator ==(Card c, int i)
    {
        return c.Value == i;
    }

    /// <summary>
    ///     surcharge de l'opérateur de non égalité
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator !=(Card c, int i)
    {
        return !(c == i);
    }

    /// <summary>
    ///     surcharge de l'opérateur de plus petit que entre deux cartes
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator <(Card c1, Card c2)
    {
        return c1.Value < c2.Value;
    }

    /// <summary>
    ///     surcharge de l'opérateur plus grand que entre deux cartes
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator >(Card c1, Card c2)
    {
        return c1.Value > c2.Value;
    }

    /// <summary>
    ///     surcharge de l'opérateur de plus petit que entre une carte et un int
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator <(Card c1, int i)
    {
        return c1.Value < i;
    }

    /// <summary>
    ///     surcharge de l'opérateur plus grand que entre une carte et un int
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator >(Card c1, int i)
    {
        return c1.Value > i;
    }
}