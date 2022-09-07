namespace KingSkullClassicOnline.Engine;

using Game;

/// <summary>
///     Mets à disposition des méthodes statiques
/// </summary>
public static class ScoreCalculator
{
    /// <summary>
    ///     Met à jour le score d'un joueur en fonction de ses résultats après une manche
    /// </summary>
    /// <param name="p">joueur concerné</param>
    /// <param name="folds">résultats des plis effectués pendant la manche</param>
    /// <param name="turn">tour correspondant à la manche</param>
    public static void UpdateScore(Player p, Fold[] folds, int turn)
    {
        int bonusPoints = 0, wonFold = 0, score;

        foreach (var fold in folds)
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

        var vote = p.GetVote(turn)!.Voted;
        if (wonFold == vote)
        {
            if (vote == 0)
                score = Config.Score0 * turn + bonusPoints;
            else
                score = Config.ScoreVoted * vote + bonusPoints;
        }
        else
        {
            if (vote == 0)
                score = -(Config.Score0 * turn);
            else
                score = Config.ScoreBadVote * Math.Abs(vote - wonFold);
        }

        p.SetTotal(turn,score);
    }
}