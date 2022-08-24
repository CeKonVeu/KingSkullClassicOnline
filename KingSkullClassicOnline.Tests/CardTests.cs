using GameLogic;
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

        p1 = new Player("Loic");
        p2 = new Player("Alex");
        p3 = new Player("Loris");
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

        Assert.Contains(p1, Controller.Players);
        Assert.Contains(p2, Controller.Players);
        Assert.Contains(p3, Controller.Players);
    }

    [Test]
    public void ItShouldRemovePlayers()
    {
        _controller.AddPlayer(p3);
        Assert.Contains(p3, Controller.Players);
        _controller.RemovePlayer(p3);
        CollectionAssert.DoesNotContain(Controller.Players, p3);
    }

    [Test]
    public void ItShouldDealCardsCorrectly()
    {
        _controller.AddPlayer(p1);
        _controller.AddPlayer(p2);
        _controller.DealCards();
        Assert.AreEqual(Controller.Turn, p1.Hand.Count);
        Assert.AreEqual(Controller.Turn, p2.Hand.Count);
    }
}