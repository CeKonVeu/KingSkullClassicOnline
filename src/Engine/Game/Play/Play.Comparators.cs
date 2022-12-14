namespace KingSkullClassicOnline.Engine.Game;

public partial record Play
{
    public bool HasSameColorAs(Play other)
    {
        return Card.Color == other.Card.Color;
    }

    public bool IsBlack()
    {
        return Card.Color == Color.Black;
    }

    public bool IsEscape()
    {
        return Card == Config.EscapeValue;
    }

    public bool IsMermaid()
    {
        return Card == Config.MermaidValue;
    }

    public bool IsPirate()
    {
        return Card == Config.PirateValue;
    }

    public bool IsScaryMary()
    {
        return Card.IsScaryMary();
    }

    public bool IsSkullKing()
    {
        return Card == Config.SkullKingValue;
    }

    public bool IsSpecial()
    {
        return Card.IsSpecial();
    }

    public bool IsStrongerThan(Play other)
    {
        return Card > other.Card;
    }
}