namespace GameLogic;

/// <summary>
///     gère une manche de jeu
/// </summary>
public class Round
{
    private readonly Fold[] _plis;
    private int[] _votes;

    /// <summary>
    ///     constructeur
    /// </summary>
    /// <param name="numPlayers">nombre de joueurs dans la partie</param>
    /// <param name="turn">tour du round</param>
    public Round(int numPlayers, int turn)
    {
        _votes = new int[numPlayers];
        _plis = new Fold[turn];
    }

    /// <summary>
    ///     ajoute les votes aux joueurs
    /// </summary>
    /// <param name="votes"></param>
    public void addVotes(int[] votes)
    {
        _votes = votes;
    }

    /// <summary>
    ///     mets à jour le score de chaque joueurs selon les résultats des plis
    /// </summary>
    public void UpdateScore()
    {
        for (var i = 0; i < Controller.Players.Count; i++)
        {
            int bonusPoints = 0, wonFold = 0, score;

            foreach (var fold in _plis)
                if (fold.GetWinner().Player == Controller.Players[i])
                {
                    wonFold++;
                    if (fold.GetWinner().Card == Config.MermaidValue && fold.HasSkullKing)
                        bonusPoints += Config.BonusMermaid;
                    else if (fold.GetWinner().Card == Config.SkullKingValue)
                        bonusPoints += fold.GetNumberPirate() * Config.BonusSkullKing;
                }

            if (wonFold == _votes[i])
                if (_votes[i] == 0)
                    score = bonusPoints + Config.Score0 * Controller.Turn;
                else
                    score = bonusPoints + Config.ScoreVoted * _votes[i];
            else if (_votes[i] == 0)
                score = -(Config.Score0 * Controller.Turn);
            else
                score = Config.ScoreBadVote * Math.Abs(_votes[i] - wonFold);

            Controller.Players[i].AddScore(_votes[i], score);
        }
    }
}