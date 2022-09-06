namespace KingSkullClassicOnline.Engine;

using Game;

/// <summary>
///     Mets à disposition des méthodes statiques
/// </summary>
public static class ScoreCalculator
{
    /// <summary>
    ///     met à jour le score d'un joueur en fonction de ses résultats après une manche
    /// </summary>
    /// <param name="p">joueur concerné</param>
    /// <param name="plis">résultats des plis effectués pendant la manche</param>
    /// <param name="vote">vote du joueur</param>
    /// <param name="turn">tour correspondant à la manche</param>
    public static void UpdateScore(Player p, Fold[] plis, int turn)
    {
        int bonusPoints = 0, wonFold = 0, score;

        foreach (var fold in plis)
        {
            var winner = fold.GetWinner();
            if (winner.Player == p)
            {
                wonFold++;
                if (winner.Card == Config.MermaidValue && fold.HasSkullKing)
                    bonusPoints += Config.BonusMermaid;
                else if (winner.Card == Config.SkullKingValue)
                    bonusPoints += fold.CountPirates() * Config.BonusSkullKing;
            }
        }

        if (wonFold == p.GetVote(turn)!.Voted)
            if (p.GetVote(turn)!.Voted == 0)
                score = bonusPoints + Config.Score0 * turn;
            else
                score = bonusPoints + Config.ScoreVoted * p.GetVote(turn)!.Voted;
        else if (p.GetVote(turn)!.Voted == 0)
            score = -(Config.Score0 * turn);
        else
            score = Config.ScoreBadVote * Math.Abs(p.GetVote(turn)!.Voted - wonFold);

        p.SetTotal(turn,score);
        //p.SetVote(turn, vote, score);
    }
}