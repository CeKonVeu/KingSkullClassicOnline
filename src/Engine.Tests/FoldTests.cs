using System.Linq;
using KingSkullClassicOnline.Engine.Cards;
using KingSkullClassicOnline.Engine.Game;
using NUnit.Framework;

namespace KingSkullClassicOnline.Engine.Tests;

public class FoldTests
{
    private Fold _fold;
    private Player _p1, _p2;
    
    // TODO : Tester la ScaryMary

    private void playTestCards(Card c1, Card c2, Card winningCard)
    {
        _p1.Hand.Add(c1);
        _p2.Hand.Add(c2);

        _fold.PlayCard(_p1, c1);
        _fold.PlayCard(_p2, c2);

        Assert.AreEqual(_fold.GetWinner().Card, winningCard);
    }
    
    // Tests d'atouts //
    
    [Test]
    public void TheCardWithTheHighestValueShouldWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Red);

        playTestCards(c1, c2, c2);
    }
    
    [Test]
    public void OnlyNumberedCardsOfTheTurnColorShouldWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Blue);
        
        playTestCards(c1, c2, c1);
    }
    
    [Test]
    public void BlackCardsShouldWinEvenIfTheTurnColorIsNotBlack()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Black);
        
        playTestCards(c1, c2, c2);
    }

    // Tests d'égalités //
    
    [Test]
    public void TheFirstCardPlayedShouldWinIfTheyHaveTheSameValue()
    {
        var c1 = Card.Pirate(1);
        var c2 = Card.Pirate(2);
        
        playTestCards(c1, c2, c1);
    }
    
    [Test]
    public void TheFirstEscapePlayedShouldWinIfThereAreOnlyEscapes()
    {
        var c1 = Card.Escape();
        var c2 = Card.Escape();

        playTestCards(c1, c2, c1);
    }
    
    // Setup //

    [SetUp]
    public void Setup()
    {
        _fold = new Fold();
        _p1 = new Player("1", "");
        _p2 = new Player("2", "");
    }
}