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

    // Test sur les cartes numérotées //

    [Test]
    public void HighestRedCardShouldWinIfTheTurnColorIsRed()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Red);

        PlayTestCardsBothWays(c1, c2);
    }

    [Test]
    public void HighestBlueCardShouldWinIfTheTurnColorIsBlue()
    {
        var c1 = Card.NumberedCard(13, Colors.Blue);
        var c2 = Card.NumberedCard(1, Colors.Blue);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void HighestYellowCardShouldWinIfTheTurnColorIsYellow()
    {
        var c1 = Card.NumberedCard(13, Colors.Yellow);
        var c2 = Card.NumberedCard(1, Colors.Yellow);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void HighestBlackCardShouldWinIfTheTurnColorIsBlack()
    {
        var c1 = Card.NumberedCard(13, Colors.Black);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTestCardsBothWays(c1, c2);
    }
    
    // Test sur l'atout (rouge) //
        
    [Test]
    public void IfTheTurnColorIsRedBlueCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCards(c1, c2, true);
    }
    
    [Test]
    public void IfTheTurnColorIsRedYellowCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTestCards(c1, c2, true);
    }
    
    // Test sur l'atout (bleu) //

    [Test]
    public void IfTheTurnColorIsBlueRedCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Blue);
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCards(c1, c2, true);
    }
    
    [Test]
    public void IfTheTurnColorIsBlueYellowCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Blue);
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTestCards(c1, c2, true);
    }

    // Test sur l'atout (jaune) //

    [Test]
    public void IfTheTurnColorIsYellowRedCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Yellow);
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCards(c1, c2, true);
    }

    [Test]
    public void IfTheTurnColorIsYellowBlueCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Yellow);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCards(c1, c2, true);
    }

    // Test de l'atout (noir) //
        
    [Test]
    public void IfTheTurnColorIsBlackRedCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCards(c1, c2, true);
    }
    
    [Test]
    public void IfTheTurnColorIsBlackBlueCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCards(c1, c2, true);
    }
    
    [Test]
    public void IfTheTurnColorIsBlackYellowCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTestCards(c1, c2, true);
    }
    
    // Test sur les cartes noires //
    
    [Test]
    public void BlackCardsShouldWinEvenIfTheTurnColorIsRed()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTestCards(c1, c2, false);
    }
    
    [Test]
    public void BlackCardsShouldWinEvenIfTheTurnColorIsBlue()
    {
        var c1 = Card.NumberedCard(13, Colors.Blue);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTestCards(c1, c2, false);
    }
    
    [Test]
    public void BlackCardsShouldWinEvenIfTheTurnColorIsYellow()
    {
        var c1 = Card.NumberedCard(13, Colors.Yellow);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTestCards(c1, c2, false);
    }
    
    [Test]
    public void HighestBlackCardShouldWinEvenIfTheTurnColorIsNotBlack()
    {
        // Cas où la carte plus haute est jouée en dernier
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Black);
        var c3 = Card.NumberedCard(5, Colors.Black);

        _p1.Hand.Add(c1);
        _p2.Hand.Add(c2);
        _p3.Hand.Add(c3);

        _fold.PlayCard(_p1, c1);
        _fold.PlayCard(_p2, c2);
        _fold.PlayCard(_p3, c3);

        Assert.AreEqual(_fold.GetWinner().Card, c3);
        
        // Cas où la carte plus haute est jouée avant une autre noire
        ResetFold();
        var c4 = Card.NumberedCard(13, Colors.Red);
        var c5 = Card.NumberedCard(5, Colors.Black);
        var c6 = Card.NumberedCard(1, Colors.Black);

        _p1.Hand.Add(c4);
        _p2.Hand.Add(c5);
        _p3.Hand.Add(c6);

        _fold.PlayCard(_p1, c4);
        _fold.PlayCard(_p2, c5);
        _fold.PlayCard(_p3, c6);

        Assert.AreEqual(_fold.GetWinner().Card, c5);
    }

    // Tests des escapes //

    [Test]
    public void ARedCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void ABlueCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Blue);
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }

    [Test]
    public void AYellowCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Yellow);
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

    // Test des sirènes //

    [Test]
    public void AMermaidShouldWinAgainstAnEscape()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void AMermaidShouldWinAgainstRedCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void AMermaidShouldWinAgainstBlueCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void AMermaidShouldWinAgainstYellowCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void AMermaidShouldWinAgainstBlackCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Black);

        PlayTestCardsBothWays(c1, c2);
    }
    
    // Test des pirates //
    
    [Test]
    public void APirateShouldWinAgainstAnEscape()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }

    [Test]
    public void APirateShouldWinAgainstRedCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void APirateShouldWinAgainstBlueCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void APirateShouldWinAgainstYellowCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void APirateShouldWinAgainstBlackCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Black);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void APirateShouldWinAgainstAMermaid()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Mermaid();

        PlayTestCardsBothWays(c1, c2);
    }
    
    // Test du Skull King //

    [Test]
    public void ASkullKingShouldWinAgainstAnEscape()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Escape();

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void ASkullKingShouldWinAgainstRedCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void ASkullKingShouldWinAgainstBlueCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void ASkullKingShouldWinAgainstYellowCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void ASkullKingShouldWinAgainstBlackCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Black);

        PlayTestCardsBothWays(c1, c2);
    }
    
    [Test]
    public void ASkullKingShouldWinAgainstAPirate()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Pirate();

        PlayTestCardsBothWays(c1, c2);
    }

    // Cas particulier //
    
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