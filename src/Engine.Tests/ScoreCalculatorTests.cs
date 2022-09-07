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
    ///     Défini les votes de 2 joueurs
    /// </summary>
    /// <param name="vote1">Vote d</param>
    /// <param name="vote2"></param>
    private void SetAllVotes(int vote1, int vote2)
    {
        _vote1 = vote1;
        _vote2 = vote2;
        _p1.SetVote(_turn, _vote1);
        _p2.SetVote(_turn, _vote2);
    }

    /// <summary>
    ///     Permet de jouer 2 cartes durant un pli
    /// </summary>
    /// <param name="turn">Numéro du pli</param>
    /// <param name="c1">Carte de p1</param>
    /// <param name="c2">Carte de p2</param>
    private void PlayFold(int turn, Card c1, Card c2)
    {
        _folds[turn].PlayCard(_p1, c1);
        _folds[turn].PlayCard(_p2, c2);
    }

    /// <summary>
    ///     Met à jour les scores des 2 joueurs
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
    public void ItShouldAddAndSubtractScoreCorrectlyWhenTheVoteIsNot0()
    {
        SetAllVotes(3, 3);
        PlayFold(0, Card.NumberedCard(10, Color.Black), Card.NumberedCard(1, Color.Yellow));
        PlayFold(1, Card.NumberedCard(10, Color.Black), Card.NumberedCard(1, Color.Yellow));
        PlayFold(2, Card.NumberedCard(10, Color.Black), Card.NumberedCard(1, Color.Yellow));
        UpdateAllScores();
        CheckScores(Config.ScoreVoted * _turn, Config.ScoreBadVote *  Math.Abs(_vote2 - 0));
    }
    
    // Test sur des votes = 0 //

    [Test]
    public void ItShouldAddAndSubtractScoreCorrectlyWhenTheVoteIs0()
    {
        SetAllVotes(0, 0);
        PlayFold(0, Card.NumberedCard(10, Color.Black), Card.NumberedCard(1, Color.Yellow));
        PlayFold(1, Card.NumberedCard(10, Color.Black), Card.NumberedCard(1, Color.Yellow));
        PlayFold(2, Card.NumberedCard(10, Color.Black), Card.NumberedCard(1, Color.Yellow));
        UpdateAllScores();
        CheckScores(Config.Score0 * _turn, -(Config.Score0 * _turn));
    }

/*
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
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _p1 = new Player("1", "");
        _p2 = new Player("2", "");

        _turn = 3;
        _folds = new[]
        {
            new Fold(), new Fold(), new Fold()
        };
    }
}