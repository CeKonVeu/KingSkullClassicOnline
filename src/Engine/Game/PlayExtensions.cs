namespace KingSkullClassicOnline.Engine.Game;

public static class PlayExtensions
{
    public static bool HasSameColorAs(this Play play, Play other)
    {
        return play.Card.Color == other.Card.Color;
    }

    public static bool IsBlack(this Play play)
    {
        return play.Card.Color == Colors.Black;
    }

    public static bool IsEscape(this Play play)
    {
        return play.Card == Config.EscapeValue;
    }

    public static bool IsMermaid(this Play play)
    {
        return play.Card == Config.MermaidValue;
    }

    public static bool isPirate(this Play play)
    {
        return play.Card == Config.PirateValue;
    }

    public static bool IsSkullKing(this Play play)
    {
        return play.Card == Config.SkullKingValue;
    }

    public static bool IsSpecial(this Play play)
    {
        return play.Card.Color == Colors.None;
    }

    public static bool IsStrongerThan(this Play play, Play other)
    {
        return play.Card > other.Card;
    }
}