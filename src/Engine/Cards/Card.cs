namespace KingSkullClassicOnline.Engine.Cards;

/// <summary>
///     Carte de jeu
/// </summary>
public partial class Card
{
    /// <summary>
    ///     constructeur
    /// </summary>
    /// <param name="value">valeur de la carte</param>
    /// <param name="name"></param>
    /// <param name="color"></param>
    private Card(int value, string name, Color color = Color.None)
    {
        Color = color;
        Name = name;
        Value = value;
        IsPlayable = false;
    }

    // Nécessaire pour l'envoi via SignalR
    public Card()
    {
    }

    /// <summary>
    ///     couleur de la carte
    /// </summary>
    internal Color Color { get; }

    public bool IsPlayable { get; set; }

    /// <summary>
    ///     nom représentant la carte
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     valeur de la carte
    /// </summary>
    internal int Value { get; }

    private bool Equals(Card other)
    {
        return Color == other.Color && Name == other.Name && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Card)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Color, Value, Name);
    }

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
    ///     surcharge de l'opérateur plus grand que entre une carte et un int
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator >(Card c1, int i)
    {
        return c1.Value > i;
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
    ///     surcharge de l'opérateur de plus petit que entre une carte et un int
    /// </summary>
    /// <param name="c">carte à comparer</param>
    /// <param name="i">valeur de comparaison</param>
    /// <returns>le résultat de la comparaison</returns>
    public static bool operator <(Card c1, int i)
    {
        return c1.Value < i;
    }
}