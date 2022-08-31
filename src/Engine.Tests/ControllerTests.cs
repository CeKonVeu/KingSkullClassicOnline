using NUnit.Framework;

namespace KingSkullClassicOnline.Engine.Tests;

public class ControllerTests
{
    private int _cardsInDeck;
    private Controller _controller = null!;

    private Player p1, p2, p3;

    [SetUp]
    public void Setup()
    {
        _controller = new Controller();
        _cardsInDeck = Config.NumberEscapes + Config.NumberMermaids + Config.NumberPirates + Config.NumberSkullKing +
                       4 * Config.NumberNumCards + Config.NumberScaryM;

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
        Assert.Contains(p1, _controller.Players);
        Assert.Contains(p2, _controller.Players);
        Assert.Contains(p3, _controller.Players);
    }

    [Test]
    public void ItShouldRemovePlayers()
    {
        Assert.Contains(p3, _controller.Players);
        _controller.RemovePlayer(p3);
        CollectionAssert.DoesNotContain(_controller.Players, p3);
    }
    
    [Test]
    public void ItShouldRemovePlayersByNames()
    {
        Assert.Contains(p3, _controller.Players);
        _controller.RemovePlayer(p3.Name);
        CollectionAssert.DoesNotContain(_controller.Players, p3);
    }

    [Test]
    public void ItShouldRespectTheGameConfig()
    {
        _controller.StartGame();
        Assert.AreEqual(_controller.Turn, Config.TurnNumber + 1);
    }
}