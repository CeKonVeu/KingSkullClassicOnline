using System.Linq;
using KingSkullClassicOnline.Engine.Cards;
using KingSkullClassicOnline.Engine.Game;
using NUnit.Framework;

namespace KingSkullClassicOnline.Engine.Tests;

public class FoldTests
{
    private Fold _fold;
    private Player _p1, _p2;

    [Test]
    public void TheFirstCardPlayedWinIfTheyHaveTheSameValue()
    {
        var c1 = Card.Pirate(1);
        var c2 = Card.Pirate(2);
        
        _p1.Hand.Add(c1);
        _p2.Hand.Add(c2);

        _fold.PlayCard(_p1, _p1.Hand.First());
        _fold.PlayCard(_p2, _p2.Hand.First());

        Assert.AreEqual(_fold.GetWinner().Card, c1);
    }

    [SetUp]
    public void Setup()
    {
        _fold = new Fold();
        _p1 = new Player("1", "");
        _p2 = new Player("2", "");
    }
}