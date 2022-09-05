namespace KingSkullClassicOnline.Engine.Game;

public partial record Play
{
    public bool HasSameColorAs(Play other)
    {
        return Card.Color == other.Card.Color;
    }

    public bool IsBlack()
    {
        return Card.Color == Colors.Black;
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

    public bool IsSkullKing()
    {
        return Card == Config.SkullKingValue;
    }

    public bool IsSpecial()
    {
        return Card.Color == Colors.None;
    }

    public bool IsStrongerThan(Play other)
    {
        return Card > other.Card;
    }
}