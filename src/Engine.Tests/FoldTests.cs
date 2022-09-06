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
    ///     Réinitalise l'objet fold afin de tester un nouveau pli.
    /// </summary>
    private void ResetFold()
    {
        _fold = new Fold();
    }

    /// <summary>
    ///     Simule un pli où 2 cartes sont jouées et teste si la carte gagnante est correcte.
    /// </summary>
    /// <param name="c1">Première carte jouée</param>
    /// <param name="c2">Deuxième carte jouée</param>
    /// <param name="firstWins">True si la première carte gagne, false sinon</param>
    private void PlayTestCards(Card c1, Card c2, bool firstWins)
    {
        _p1.Hand.Add(c1);
        _p2.Hand.Add(c2);
    
        _fold.PlayCard(_p1, c1);
        _fold.PlayCard(_p2, c2);

        Assert.AreSame(_fold.GetWinner().Card, firstWins ? c1 : c2);
        Assert.AreNotSame(_fold.GetWinner().Card, firstWins ? c2 : c1);
    }
    
    /// <summary>
    ///     Simule deux plis où 2 cartes sont jouées, dans un sens puis dans l'autre,
    ///     et teste si la carte gagnante est correcte.
    ///     La carte qui gagne doit toujours être la même, indépendamment de l'ordre des joueurs.
    /// </summary>
    /// <param name="winningCard">La carte qui devrait gagner</param>
    /// <param name="losingCard">La carte qui devrait perdre</param>
    private void PlayTestCardsBothWays(Card winningCard, Card losingCard)
    {
        PlayTestCards(winningCard, losingCard, true);
        ResetFold();
        PlayTestCards(losingCard, winningCard, false);
    }

    // Tests des cartes de couleur //

    [Test]
    public void TheRedCardWithTheHighestValueShouldWinIfTheTurnColorIsRed()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Red);

        PlayTestCardsBothWays(c1, c2);
    }

    [Test]
    public void OnlyNumberedCardsOfTheTurnColorShouldWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCards(c1, c2, true);
        ResetFold();
        PlayTestCards(c2, c1, true);
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

        PlayTestCardsBothWays(c1, c2);
    }

    // Tests des escapes //

    [Test]
    public void AColoredCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ABlackCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void AMermaidShouldWinAgainstAnEscape()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void APirateShouldWinAgainstAnEscape()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstAnEscape()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }
    
    // Test des sirènes //

    [Test]
    public void AMermaidShouldWinAgainstColoredCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCardsBothWays(c1, c2);
    }
    
    // Test des pirates //

    [Test]
    public void APirateShouldWinAgainstAMermaid()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Mermaid();

        PlayTestCardsBothWays(c1, c2);
    }
    
    // Test du Skull King //

    [Test]
    public void ASkullKingShouldWinAgainstAPirate()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Pirate();

        PlayTestCardsBothWays(c1, c2);
    }

    [Test]
    public void AMermaidShouldWinAgainstASkullKing()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.SkullKing();

        PlayTestCardsBothWays(c1, c2);
    }

    // Tests d'égalités //

    [Test]
    public void TheFirstEscapePlayedShouldWinIfThereAreOnlyEscapes()
    {
        var c1 = Card.Escape();
        var c2 = Card.Escape();

        PlayTestCards(c1, c2, true);
        ResetFold();
        PlayTestCards(c2, c1, true);
    }

    [Test]
    public void TheFirstMermaidPlayedShouldWin()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Mermaid();

        PlayTestCards(c1, c2, true);
        ResetFold();
        PlayTestCards(c2, c1, true);
    }

    [Test]
    public void TheFirstPiratePlayedShouldWin()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Pirate();

        PlayTestCards(c1, c2, true);
        ResetFold();
        PlayTestCards(c2, c1, true);
    }

    [Test]
    public void TheFirstSkullKingPlayedShouldWin()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.SkullKing();

        PlayTestCards(c1, c2, true);
        ResetFold();
        PlayTestCards(c2, c1, true);;
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