namespace KingSkullClassicOnline.Engine.Cards;

public partial class Card
{
    public static Card SkullKing()
    {
        return new Card(Config.SkullKingValue, "SkullKing");
    }

    public static Card Pirate(int variant)
    {
        if (variant <= 0 || variant > Config.PirateVariants)
            throw new ArgumentOutOfRangeException("Variante de la carte n'existe pas");

        return new Card(Config.PirateValue, "Pirate_" + variant);
    }

    public static Card ScaryMary()
    {
        // TODO : Permettre à la ScaryMary de valoir soit Pirate soit Escape
        return new Card(Config.PirateValue, "ScaryMary");
    }

    public static Card Mermaid()
    {
        return new Card(Config.MermaidValue, "Mermaid");
    }

    public static Card Escape()
    {
        return new Card(Config.EscapeValue, "Escape");
    }

    public static Card NumberedCard(int value, Colors color)
    {
        if (value <= 0 || value > Config.NumberNumCards)
            throw new ArgumentOutOfRangeException("Valeur de la carte n'existe pas");

        string? name;
        switch (color)
        {
            case Colors.Red:
                name = "Red_";
                break;
            case Colors.Blue:
                name = "Blue_";
                break;
            case Colors.Yellow:
                name = "Yellow_";
                break;
            case Colors.Black:
                name = "Black_";
                break;
            default:
                throw new ArgumentException("Couleur de la carte non définie");
        }

        return new Card(value, name + value, color);
    }
}