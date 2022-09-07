using System;
using KingSkullClassicOnline.Engine.Cards;
using KingSkullClassicOnline.Engine.Game;
using NUnit.Framework;

namespace KingSkullClassicOnline.Engine.Tests;

public class ScoreCalculatorTests
{
    private Fold[] _folds = null!;
    private Player _p1 = null!, _p2 = null!;
    private int _turn, _vote1, _vote2;

    /// <summary>
    ///     Permet de jouer le dernier pli de la manche 3
    /// </summary>
    /// <param name="c1">Carte de p1</param>
    /// <param name="c2">Carte de p2</param>
    private void PlayLastFold(Card c1, Card c2)
    {
        _folds[2].PlayCard(_p1, c1);
        _folds[2].PlayCard(_p2, c2);
    }

    /// <summary>
    ///     Met à jour les scores de tous les joueurs
    /// </summary>
    private void UpdateAllScores()
    {
        ScoreCalculator.UpdateScore(_p1, _folds, _turn);
        ScoreCalculator.UpdateScore(_p2, _folds, _turn);
    }

    /// <summary>
    ///     Vérifie que les scores obtenus et les scores attendus correspondent
    /// </summary>
    /// <param name="score1">Score attendu de p1</param>
    /// <param name="score2">Score attendu de p2</param>
    private void CheckScores(int score1, int score2)
    {
        Assert.AreEqual(_p1.GetVote(_turn)!.Total, _p1.GetTotal(_turn));
        Assert.AreEqual(_p2.GetVote(_turn)!.Total, _p2.GetTotal(_turn));
    }
    
    // Test sur des votes > 1 //
    
    [Test]
    public void ItShouldAddAndSubtractScoreCorrectlyWhenVoteIsNot0()
    {
        PlayLastFold(Card.NumberedCard(10, Color.Black), Card.NumberedCard(1, Color.Yellow));
        UpdateAllScores();
        CheckScores(_turn * Config.ScoreVoted, Math.Abs(_vote2 - 0) * Config.ScoreBadVote);
    }
    
/*
    // Test sur des votes = 0 //

    [Test]
    public void ItShouldCountScoreCorrectlyWhen0()
    {
        _vote1 = 0;
        _vote2 = 0;

        _folds[2].PlayCard(_p1, new NumberedCard(10, "a", Colors.Black));
        _folds[2].PlayCard(_p2, new NumberedCard(0, "a", Colors.Black));
        ScoreCalculator.UpdateScore(_p1, _folds, _vote1, _turn);
        ScoreCalculator.UpdateScore(_p2, _folds, _vote2, _turn);

        Assert.AreEqual(_p1.Votes[_turn - 1], (_vote1, -(_turn * Config.Score0)));
        Assert.AreEqual(_p2.Votes[_turn - 1], (_vote2, _turn * Config.Score0));
    }

    [Test]
    public void ItShouldGiveBonusForMermaidOnSkullKing()
    {
        _vote1 = 3;
        _vote2 = 0;

        _folds[2].PlayCard(_p1, new SpecialCard(14, "a"));
        _folds[2].PlayCard(_p2, new SpecialCard(16, "a"));
        ScoreCalculator.UpdateScore(_p1, _folds, _vote1, _turn);
        ScoreCalculator.UpdateScore(_p2, _folds, _vote2, _turn);

        Assert.AreEqual(_p1.Votes[_turn - 1], (_vote1, _turn * Config.ScoreVoted + Config.BonusMermaid));
        Assert.AreEqual(_p2.Votes[_turn - 1], (_vote2, _turn * Config.Score0));
    }

    [Test]
    public void ItShouldGiveBonusForSkullKingOnPirate()
    {
        _vote1 = 3;
        _vote2 = 0;

        _folds[2].PlayCard(_p1, new SpecialCard(16, "a"));
        _folds[2].PlayCard(_p2, new SpecialCard(15, "a"));
        ScoreCalculator.UpdateScore(_p1, _folds, _vote1, _turn);
        ScoreCalculator.UpdateScore(_p2, _folds, _vote2, _turn);

        Assert.AreEqual(_p1.Votes[_turn - 1], (_vote1, _turn * Config.ScoreVoted + Config.BonusSkullKing));
        Assert.AreEqual(_p2.Votes[_turn - 1], (_vote2, _turn * Config.Score0));
    }

    [Test]
    public void ItShouldntGiveBonusIfVoteIsWrong()
    {
        _vote1 = 2;
        _vote2 = 0;

        _folds[2].PlayCard(_p1, new SpecialCard(16, "a"));
        _folds[2].PlayCard(_p2, new SpecialCard(15, "a"));
        ScoreCalculator.UpdateScore(_p1, _folds, _vote1, _turn);
        ScoreCalculator.UpdateScore(_p2, _folds, _vote2, _turn);

        Assert.AreEqual(_p1.Votes[_turn - 1], (_vote1, Math.Abs(_vote1 - _turn) * Config.ScoreBadVote));
        Assert.AreEqual(_p2.Votes[_turn - 1], (_vote2, _turn * Config.Score0));
    }
*/
    /// <summary>
    /// 2 joueurs, tour 3.
    /// A la fin des 2 premiers plis, p1 gagne 2/3 plis, p2 gagne 0/2 plis
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _p1 = new Player("1", "");
        _p2 = new Player("2", "");

        _turn = 3;
        _vote1 = 3;
        _vote2 = 2;
        
        _p1.SetVote(_turn, _vote1);
        _p2.SetVote(_turn, _vote2);
        
        _folds = new[]
        {
            new Fold(), new Fold(), new Fold()
        };

        _folds[0].PlayCard(_p1, Card.NumberedCard(10, Color.Black));
        _folds[0].PlayCard(_p2, Card.NumberedCard(1, Color.Yellow));

        _folds[1].PlayCard(_p1, Card.NumberedCard(10, Color.Black));
        _folds[1].PlayCard(_p2, Card.NumberedCard(1, Color.Yellow));
    }
}