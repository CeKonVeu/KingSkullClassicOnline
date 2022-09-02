namespace KingSkullClassicOnline.Engine.Tests;

using Game;
using NUnit.Framework;

public class RoundTests
{
    private Controller _controller = null!;

    private Player _p1 = null!, _p2 = null!, _p3 = null!;
    private Round _round = null!;

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
        CollectionAssert.AreEqual(new[] { 0, 0, 0 }, _round.Votes);

        foreach (var pli in _round.Plis) Assert.AreEqual(_controller.Players.Count, pli.CardsPlayed.Count);
    }

    [SetUp]
    public void Setup()
    {
        _controller = new Controller();

        _p1 = new Player("1", "Loic");
        _p2 = new Player("2", "Alex");
        _p3 = new Player("3", "Loris");

        _controller.AddPlayers(_p1, _p2, _p3);

        _controller.Turn = 2;

        _round = new Round(_controller);
    }
}