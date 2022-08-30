namespace KingSkullClassicOnline.Engine;

/// <summary>
///     contient les différentes constantes nécessaires dans le programme
/// </summary>
public static class Config
{
    public const int NumberNumCards = 13;
    public const int NumberEscapes = 5;
    public const int NumberPirates = 5;
    public const int NumberSkullKing = 1;
    public const int NumberScaryM = 1;
    public const int NumberMermaids = 2;
    public const int MaxPlayer = 6;
    public const int EscapeValue = 0;
    public const int MermaidValue = NumberNumCards + 1;
    public const int PirateValue = MermaidValue + 1;
    public const int SkullKingValue = PirateValue + 1;
    public const int ScoreVoted = 20;
    public const int Score0 = 10;
    public const int ScoreBadVote = -10;
    public const int BonusMermaid = 50;
    public const int BonusSkullKing = 30;
    public const int TurnNumber = 10;
}