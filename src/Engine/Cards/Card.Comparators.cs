using KingSkullClassicOnline.Shared;

namespace KingSkullClassicOnline.Engine.Cards;

public partial class Card
{
    public bool IsSpecial()
    {
        return Color == Color.None;
    }

    public bool IsScaryMary()
    {
        return Name == CardNames.ScaryMary;
    }

    public bool IsScaryMaryPirate()
    {
        return Name == CardNames.ScaryMaryPirate;
    }
}