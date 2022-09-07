namespace KingSkullClassicOnline.Engine.Cards;

public partial class Card
{
    public bool IsSpecial()
    {
        return Color == Color.None;
    }
}