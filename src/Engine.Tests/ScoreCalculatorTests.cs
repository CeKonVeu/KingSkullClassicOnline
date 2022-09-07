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
        
        var (winner, _) = _folds[turn].GetWinner();
        winner.AddActual(_turn);
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
        Assert.AreEqual(_p1.GetTotal(_turn), score1);
        Assert.AreEqual(_p2.GetTotal(_turn), score2);
    }
    
    private int GetWinVoteNot0(int vote)
    {
        return Config.ScoreVoted * vote;
    }

    private int GetLoseVoteNot0(int vote, Player p)
    {
        return Config.ScoreBadVote * Math.Abs(vote - p.GetVote(_turn)!.Actual);
    }

    private int GetVote0(bool win)
    {
        return Config.Score0 * _turn * (win ? 1 : -1);
    }

    // Test sur des votes > 1 //
    
    [Test]
    public void ItShouldAddAndSubtractScoreCorrectlyWhenTheVoteIsNot0()
    {
        SetAllVotes(2, 2);
        PlayFold(0, Card.NumberedCard(1, Color.Yellow), Card.Escape());
        PlayFold(1, Card.NumberedCard(1, Color.Yellow), Card.Escape());
        PlayFold(2, Card.Escape(), Card.NumberedCard(1, Color.Yellow));
        UpdateAllScores();
        CheckScores(GetWinVoteNot0(_vote1), GetLoseVoteNot0(_vote2, _p2));
    }
    
    // Test sur des votes = 0 //

    [Test]
    public void ItShouldAddAndSubtractScoreCorrectlyWhenTheVoteIs0()
    {
        SetAllVotes(0, 0);
        PlayFold(0, Card.Escape(), Card.NumberedCard(1, Color.Yellow));
        PlayFold(1, Card.Escape(), Card.NumberedCard(1, Color.Yellow));
        PlayFold(2, Card.Escape(), Card.NumberedCard(1, Color.Yellow));
        UpdateAllScores();
        CheckScores(GetVote0(true), GetVote0(false));
    }

    // Test sur les points bonus //
    
    [Test]
    public void ItShouldGiveBonusForMermaidOnSkullKing()
    {
        SetAllVotes(2, 1);
        PlayFold(0, Card.Mermaid(), Card.SkullKing());
        PlayFold(1, Card.Escape(), Card.Escape());
        PlayFold(2, Card.SkullKing(), Card.Mermaid());
        UpdateAllScores();
        CheckScores(GetWinVoteNot0(_vote1) + Config.BonusMermaid, GetWinVoteNot0(_vote2) + Config.BonusMermaid);
    }

/*
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
        
        _p1.SetVote(1, 0);
        _p2.SetVote(1, 0);
        _p1.SetVote(2, 0);
        _p2.SetVote(2, 0);
    }
}