using System;
using KingSkullClassicOnline.Engine;
using KingSkullClassicOnline.Engine.Card;
using KingSkullClassicOnline.Engine.Game;
using NUnit.Framework;

namespace LogicUnitTest;

public class UtilsTest
{
    private Player _player, _player2;
    private Fold[] plis;
    private int turn, vote, vote2;

    [SetUp]
    public void Setup()
    {
        _player = new Player("George", new Controller());
        _player2 = new Player("George2", new Controller());

        vote = 3;
        vote2 = 2;
        turn = 3;
        plis = new[]
        {
            new Fold(), new Fold(), new Fold()
        };

        plis[0].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        plis[0].PlayCard(_player2, new NumberedCard(0, "a", Colors.Yellow));
        plis[1].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        plis[1].PlayCard(_player2, new NumberedCard(0, "a", Colors.Black));
    }

    [Test]
    public void ItShouldCountScoreCorrectly()
    {
        plis[2].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        plis[2].PlayCard(_player2, new NumberedCard(0, "a", Colors.Black));
        Utils.UpdateScore(_player, plis, vote, turn);
        Utils.UpdateScore(_player2, plis, vote2, turn);

        Assert.AreEqual(_player.Votes[1], (vote, turn * Config.ScoreVoted));
        Assert.AreEqual(_player2.Votes[1], (vote2, Math.Abs(vote2 - 0) * Config.ScoreBadVote));
    }

    [Test]
    public void ItShouldCountScoreCorrectlyWhen0()
    {
        vote = 0;
        vote2 = 0;

        plis[2].PlayCard(_player, new NumberedCard(10, "a", Colors.Black));
        plis[2].PlayCard(_player2, new NumberedCard(0, "a", Colors.Black));
        Utils.UpdateScore(_player, plis, vote, turn);
        Utils.UpdateScore(_player2, plis, vote2, turn);

        Assert.AreEqual(_player.Votes[1], (vote, -(turn * Config.Score0)));
        Assert.AreEqual(_player2.Votes[1], (vote2, turn * Config.Score0));
    }

    [Test]
    public void ItShouldGiveBonusForMermaidOnSkullKing()
    {
        vote = 3;
        vote2 = 0;

        plis[2].PlayCard(_player, new SpecialCard(14, "a"));
        plis[2].PlayCard(_player2, new SpecialCard(16, "a"));
        Utils.UpdateScore(_player, plis, vote, turn);
        Utils.UpdateScore(_player2, plis, vote2, turn);

        Assert.AreEqual(_player.Votes[1], (vote, turn * Config.ScoreVoted + Config.BonusMermaid));
        Assert.AreEqual(_player2.Votes[1], (vote2, turn * Config.Score0));
    }

    [Test]
    public void ItShouldGiveBonusForSkullKingOnPirate()
    {
        vote = 3;
        vote2 = 0;

        plis[2].PlayCard(_player, new SpecialCard(16, "a"));
        plis[2].PlayCard(_player2, new SpecialCard(15, "a"));
        Utils.UpdateScore(_player, plis, vote, turn);
        Utils.UpdateScore(_player2, plis, vote2, turn);

        Assert.AreEqual(_player.Votes[1], (vote, turn * Config.ScoreVoted + Config.BonusSkullKing));
        Assert.AreEqual(_player2.Votes[1], (vote2, turn * Config.Score0));
    }

    [Test]
    public void ItShouldntGiveBonusIfVoteIsWrong()
    {
        vote = 2;
        vote2 = 0;

        plis[2].PlayCard(_player, new SpecialCard(16, "a"));
        plis[2].PlayCard(_player2, new SpecialCard(15, "a"));
        Utils.UpdateScore(_player, plis, vote, turn);
        Utils.UpdateScore(_player2, plis, vote2, turn);

        Assert.AreEqual(_player.Votes[1], (vote, Math.Abs(vote - turn) * Config.ScoreBadVote));
        Assert.AreEqual(_player2.Votes[1], (vote2, turn * Config.Score0));
    }
}