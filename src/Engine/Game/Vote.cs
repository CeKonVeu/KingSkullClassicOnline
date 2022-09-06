namespace KingSkullClassicOnline.Engine.Game;

public record Vote(int Voted)
{
    public int? Actual { get; set; } = 0;
    public int? Total { get; set; } = 0;
}