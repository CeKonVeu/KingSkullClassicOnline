namespace KingSkullClassicOnline.Engine.Cards;

using Shared;

public partial class Card
{
    public static Card Escape()
    {
        return new Card(Config.EscapeValue, "Escape");
    }

    public static Card Mermaid()
    {
        return new Card(Config.MermaidValue, "Mermaid");
    }

    public static Card NumberedCard(int value, Color color)
    {
        if (value <= 0 || value > Config.NumberNumCards)
            throw new ArgumentOutOfRangeException("Valeur de la carte n'existe pas");

        string? name;
        switch (color)
        {
            case Color.Red:
                name = "Red_";
                break;
            case Color.Blue:
                name = "Blue_";
                break;
            case Color.Yellow:
                name = "Yellow_";
                break;
            case Color.Black:
                name = "Black_";
                break;
            default:
                throw new ArgumentException("Couleur de la carte non définie");
        }

        return new Card(value, name + value, color);
    }

    public static Card Pirate(int variant = 1)
    {
        if (variant <= 0 || variant > Config.PirateVariants)
            throw new ArgumentOutOfRangeException("Variante de la carte n'existe pas");

        return new Card(Config.PirateValue, "Pirate_" + variant);
    }

    public static Card ScaryMary()
    {
        return new Card(Config.PirateValue, "ScaryMary");
    }
    
    public static Card ScaryMary(bool isPirate)
    {
        return isPirate
            ? new Card(Config.PirateValue, CardNames.ScaryMaryPirate)
                : new Card(Config.EscapeValue, CardNames.ScaryMaryEscape);
    }

    public static Card SkullKing()
    {
        return new Card(Config.SkullKingValue, "SkullKing");
    }
}