using KingSkullClassicOnline.Engine.Game;
using NUnit.Framework;

namespace KingSkullClassicOnline.Engine.Tests;

public class RoundTests
{
    private Controller _controller = null!;

    private Player _p1, _p2, _p3;
    private Round _round;

    [SetUp]
    public void Setup()
    {
        _controller = new Controller();

        _p1 = new Player("Loic", _controller);
        _p2 = new Player("Alex", _controller);
        _p3 = new Player("Loris", _controller);

        _controller.Turn = 2;

        _round = new Round(_controller);
    }

    [Test]
    public void ItShouldCreateTheCorrectRound()
    {
        Assert.AreEqual(_controller.Players.Count, _round.Votes.Length);
        Assert.AreEqual(_controller.Turn, _round.Plis.Length);
    }

    [Test]
    public void ItShouldDealCardsCorrectly()
    {
        _round.DealCards();
        Assert.AreEqual(_controller.Turn, _p1.Hand.Count);
        Assert.AreEqual(_controller.Turn, _p2.Hand.Count);
        Assert.AreEqual(_controller.Turn, _p3.Hand.Count);
    }

    [Test]
    public void ItShouldPlayCorrectly()
    {
        _round.Play();
        CollectionAssert.AreEqual(new[] {0, 0, 0}, _round.Votes);

        foreach (var pli in _round.Plis) Assert.AreEqual(_controller.Players.Count, pli.CardsPlayed.Count);
    }
}