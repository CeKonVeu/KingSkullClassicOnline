using KingSkullClassicOnline.Engine.Cards;
using KingSkullClassicOnline.Engine.Game;
using NUnit.Framework;

namespace KingSkullClassicOnline.Engine.Tests;

public class FoldTests
{
    private Fold _fold;
    private Player _p1, _p2, _p3;

    // TODO : Tester la ScaryMary

    /// <summary>
    ///     Simule 2 cartes jouées et teste si la carte gagnante est correcte.
    ///     Le pli est simulé 2 fois pour tester l'ordre des cartes.
    /// </summary>
    /// <param name="winningCard">La carte qui devrait gagner</param>
    /// <param name="losingCard">La carte qui devrait perdre</param>
    private void PlayTestCards(Card winningCard, Card losingCard)
    {
        _p1.Hand.Add(winningCard);
        _p2.Hand.Add(losingCard);

        _fold.PlayCard(_p1, winningCard);
        _fold.PlayCard(_p2, losingCard);

        Assert.AreSame(_fold.GetWinner().Card, winningCard);
        Assert.AreNotSame(_fold.GetWinner().Card, losingCard);

        _p2.Hand.Add(losingCard);
        _p1.Hand.Add(winningCard);

        _fold.PlayCard(_p2, losingCard);
        _fold.PlayCard(_p1, winningCard);

        Assert.AreSame(_fold.GetWinner().Card, winningCard);
        Assert.AreNotSame(_fold.GetWinner().Card, losingCard);
    }

    // Tests d'atouts //

    [Test]
    public void TheCardWithTheHighestValueShouldWin()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Red);

        PlayTestCards(c1, c2);
    }

    [Test]
    public void OnlyNumberedCardsOfTheTurnColorShouldWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCards(c1, c2);
    }

    [Test]
    public void HighestBlackCardShouldWinEvenIfTheTurnColorIsNotBlack()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Black);
        var c3 = Card.NumberedCard(2, Colors.Black);

        _p1.Hand.Add(c1);
        _p2.Hand.Add(c2);
        _p3.Hand.Add(c3);

        _fold.PlayCard(_p1, c1);
        _fold.PlayCard(_p2, c2);
        _fold.PlayCard(_p3, c3);

        Assert.AreEqual(_fold.GetWinner().Card, c3);
    }

    [Test]
    public void TheBlackCardWithTheHighestValueShouldWinIfTheTurnColorIsBlack()
    {
        var c1 = Card.NumberedCard(13, Colors.Black);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTestCards(c1, c2);
    }

    // Tests des escapes //

    [Test]
    public void AColoredCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.Escape();

        PlayTestCards(c1, c2);
    }

    [Test]
    public void ABlackCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.Escape();

        PlayTestCards(c1, c2);
    }
    
    [Test]
    public void AMermaidShouldWinAgainstAnEscape()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Escape();

        PlayTestCards(c1, c2);
    }
    
    [Test]
    public void APirateShouldWinAgainstAnEscape()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Escape();

        PlayTestCards(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstAnEscape()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Escape();

        PlayTestCards(c1, c2);
    }
    
    [Test]
    public void AMermaidShouldWinAgainstColoredCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCards(c1, c2);
    }

    [Test]
    public void APirateShouldWinAgainstAMermaid()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Mermaid();

        PlayTestCards(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstAPirate()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Pirate();

        PlayTestCards(c1, c2);
    }

    [Test]
    public void AMermaidShouldWinAgainstASkullKing()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.SkullKing();

        PlayTestCards(c1, c2);
    }

    // Tests d'égalités //

    [Test]
    public void TheFirstEscapePlayedShouldWinIfThereAreOnlyEscapes()
    {
        var c1 = Card.Escape();
        var c2 = Card.Escape();

        PlayTestCards(c1, c2);
    }

    [Test]
    public void TheFirstMermaidPlayedShouldWin()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Mermaid();

        PlayTestCards(c1, c2);
    }

    [Test]
    public void TheFirstPiratePlayedShouldWin()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Pirate();

        PlayTestCards(c1, c2);
    }

    [Test]
    public void TheFirstSkullKingPlayedShouldWin()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.SkullKing();

        PlayTestCards(c1, c2);
    }

    // Setup //

    [SetUp]
    public void Setup()
    {
        _fold = new Fold();
        _p1 = new Player("1", "");
        _p2 = new Player("2", "");
        _p3 = new Player("3", "");
    }
}