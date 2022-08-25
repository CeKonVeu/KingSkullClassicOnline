using KingSkullClassicOnline.Engine;
using NUnit.Framework;

namespace LogicUnitTest;

public class Tests
{
    private int _cardsInDeck;
    private Controller _controller = null!;

    private Player p1, p2, p3;

    [SetUp]
    public void Setup()
    {
        _controller = new Controller();
        _cardsInDeck = Config.NumberEscapes + Config.NumberMermaids + Config.NumberPirates + Config.NumberSkullking +
                       4 * Config.NumberNumcards + Config.NumberScaryM;

        p1 = new Player("Loic", _controller);
        p2 = new Player("Alex", _controller);
        p3 = new Player("Loris", _controller);
    }

    [Test]
    public void ItShouldCreateADeckAccordingToConfig()
    {
        Assert.AreEqual(_cardsInDeck, _controller.Deck.Count);
    }

    [Test]
    public void ItShouldAddPlayers()
    {
        _controller.AddPlayer(p1);
        _controller.AddPlayer(p2);
        _controller.AddPlayer(p3);

        Assert.Contains(p1, _controller.Players);
        Assert.Contains(p2, _controller.Players);
        Assert.Contains(p3, _controller.Players);
    }

    [Test]
    public void ItShouldRemovePlayers()
    {
        _controller.AddPlayer(p3);
        Assert.Contains(p3, _controller.Players);
        _controller.RemovePlayer(p3);
        CollectionAssert.DoesNotContain(_controller.Players, p3);
    }

    /*[Test]
    public void ItShouldDealCardsCorrectly()
    {
        _controller.AddPlayer(p1);
        _controller.AddPlayer(p2);
        _controller.DealCards();
        Assert.AreEqual(_controller.Turn, p1.Hand.Count);
        Assert.AreEqual(_controller.Turn, p2.Hand.Count);
    }*/
}