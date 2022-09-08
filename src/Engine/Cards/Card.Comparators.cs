namespace KingSkullClassicOnline.Engine.Cards;

using Shared;

public partial class Card
{
    public bool IsScaryMary()
    {
        return Name.StartsWith(CardNames.ScaryMary);
    }

    public bool IsSpecial()
    {
        return Color == Color.None;
    }
}