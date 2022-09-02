namespace KingSkullClassicOnline.Engine.Tests;

using NUnit.Framework;

public class ControllerTests
{
    private int _cardsInDeck;
    private Controller _controller = null!;

    private Player _p1 = null!, _p2 = null!, _p3 = null!;

    [Test]
    public void ItShouldAddPlayers()
    {
        Assert.Contains(_p1, _controller.Players);
        Assert.Contains(_p2, _controller.Players);
        Assert.Contains(_p3, _controller.Players);
    }

    [Test]
    public void ItShouldCreateADeckAccordingToConfig()
    {
        Assert.AreEqual(_cardsInDeck, _controller.Deck.Count);
    }

    [Test]
    public void ItShouldRemovePlayers()
    {
        Assert.Contains(_p3, _controller.Players);
        _controller.RemovePlayer(_p3);
        CollectionAssert.DoesNotContain(_controller.Players, _p3);
    }

    [Test]
    public void ItShouldRemovePlayersByNames()
    {
        Assert.Contains(_p3, _controller.Players);
        _controller.RemovePlayer(_p3.Id);
        CollectionAssert.DoesNotContain(_controller.Players, _p3);
    }

    [Test]
    public void ItShouldRespectTheGameConfig()
    {
        _controller.StartGame();
        Assert.AreEqual(_controller.Turn, Config.TurnNumber + 1);
    }

    [SetUp]
    public void Setup()
    {
        _controller = new Controller();
        _cardsInDeck = Config.NumberEscapes + Config.NumberMermaids + Config.NumberPirates + Config.NumberSkullKing +
                       4 * Config.NumberNumCards + Config.NumberScaryM;

        _p1 = new Player("1", "Loic");
        _p2 = new Player("2", "Alex");
        _p3 = new Player("3", "Loris");

        _controller.AddPlayers(_p1, _p2, _p3);
    }
}