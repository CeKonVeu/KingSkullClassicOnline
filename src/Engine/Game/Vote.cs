namespace KingSkullClassicOnline.Engine.Game;

public record Vote(int Voted)
{
    public int? Actual { get; set; } = null;
}