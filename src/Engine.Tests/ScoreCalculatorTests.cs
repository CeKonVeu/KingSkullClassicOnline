using System;
using KingSkullClassicOnline.Engine.Card;
using KingSkullClassicOnline.Engine.Game;
using NUnit.Framework;

namespace KingSkullClassicOnline.Engine.Tests;

public class ScoreCalculatorTests
{
    private Player _player = null!, _player2 = null!;
    private Fold[] _plis = null!;
    private int _turn, _vote, _vote2;

    [SetUp]
    public void Setup()
    {
        _player = new Player("George", new Controller());
        _player2 = new Player("George2", new Controller());

        _vote = 3;
        _vote2 = 2;
        _turn = 3;
        _plis = new[]
        {
            new Fold(), new Fold(), new Fold()
        };

        _plis[0].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        _plis[0].PlayCard(_player2, new NumberedCard(0, "a", Colors.Yellow));
        _plis[1].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        _plis[1].PlayCard(_player2, new NumberedCard(0, "a", Colors.Black));
    }

    [Test]
    public void ItShouldCountScoreCorrectly()
    {
        _plis[2].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        _plis[2].PlayCard(_player2, new NumberedCard(0, "a", Colors.Black));
        ScoreCalculator.UpdateScore(_player, _plis, _vote, _turn);
        ScoreCalculator.UpdateScore(_player2, _plis, _vote2, _turn);

        Assert.AreEqual(_player.Votes[0], (_vote, _turn * Config.ScoreVoted));
        Assert.AreEqual(_player2.Votes[0], (_vote2, Math.Abs(_vote2 - 0) * Config.ScoreBadVote));
    }

    [Test]
    public void ItShouldCountScoreCorrectlyWhen0()
    {
        _vote = 0;
        _vote2 = 0;

        _plis[2].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        _plis[2].PlayCard(_player2, new NumberedCard(0, "a", Colors.Black));
        ScoreCalculator.UpdateScore(_player, _plis, _vote, _turn);
        ScoreCalculator.UpdateScore(_player2, _plis, _vote2, _turn);

        Assert.AreEqual(_player.Votes[0], (_vote, -(_turn * Config.Score0)));
        Assert.AreEqual(_player2.Votes[0], (_vote2, _turn * Config.Score0));
    }

    [Test]
    public void ItShouldGiveBonusForMermaidOnSkullKing()
    {
        _vote = 3;
        _vote2 = 0;

        _plis[2].PlayCard(_player, new SpecialCard(14, "a"));
        _plis[2].PlayCard(_player2, new SpecialCard(16, "a"));
        ScoreCalculator.UpdateScore(_player, _plis, _vote, _turn);
        ScoreCalculator.UpdateScore(_player2, _plis, _vote2, _turn);

        Assert.AreEqual(_player.Votes[0], (_vote, _turn * Config.ScoreVoted + Config.BonusMermaid));
        Assert.AreEqual(_player2.Votes[0], (_vote2, _turn * Config.Score0));
    }

    [Test]
    public void ItShouldGiveBonusForSkullKingOnPirate()
    {
        _vote = 3;
        _vote2 = 0;

        _plis[2].PlayCard(_player, new SpecialCard(16, "a"));
        _plis[2].PlayCard(_player2, new SpecialCard(15, "a"));
        ScoreCalculator.UpdateScore(_player, _plis, _vote, _turn);
        ScoreCalculator.UpdateScore(_player2, _plis, _vote2, _turn);

        Assert.AreEqual(_player.Votes[0], (_vote, _turn * Config.ScoreVoted + Config.BonusSkullKing));
        Assert.AreEqual(_player2.Votes[0], (_vote2, _turn * Config.Score0));
    }

    [Test]
    public void ItShouldntGiveBonusIfVoteIsWrong()
    {
        _vote = 2;
        _vote2 = 0;

        _plis[2].PlayCard(_player, new SpecialCard(16, "a"));
        _plis[2].PlayCard(_player2, new SpecialCard(15, "a"));
        ScoreCalculator.UpdateScore(_player, _plis, _vote, _turn);
        ScoreCalculator.UpdateScore(_player2, _plis, _vote2, _turn);

        Assert.AreEqual(_player.Votes[0], (_vote, Math.Abs(_vote - _turn) * Config.ScoreBadVote));
        Assert.AreEqual(_player2.Votes[0], (_vote2, _turn * Config.Score0));
    }
}