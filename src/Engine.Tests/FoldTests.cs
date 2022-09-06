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
    private void PlayTwoTestCards(Card c1, Card c2, bool firstWins)
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
    private void PlayTwoTestCardsBothWays(Card winningCard, Card losingCard)
    {
        PlayTwoTestCards(winningCard, losingCard, true);
        ResetFold();
        PlayTwoTestCards(losingCard, winningCard, false);
    }

    /// <summary>
    ///     Simule un pli où 3 cartes sont jouées.
    /// </summary>
    /// <param name="c1">Première carte jouée</param>
    /// <param name="c2">Deuxième carte jouée</param>
    /// <param name="c3">Troisième carte jouée</param>
    private void PlayThreeTestCards(Card c1, Card c2, Card c3)
    {
        _p1.Hand.Add(c1);
        _p2.Hand.Add(c2);
        _p3.Hand.Add(c3);

        _fold.PlayCard(_p1, c1);
        _fold.PlayCard(_p2, c2);
        _fold.PlayCard(_p3, c3);
    }

    /// <summary>
    ///     Teste si une carte est gagnante sur un pli de 3 cartes
    /// </summary>
    /// <param name="winningCard">La carte qui devrait gagner</param>
    /// <param name="losingCard1">Une carte qui devrait perdre</param>
    /// <param name="losingCard2">L'autre carte qui devrait perdre</param>
    private void CheckThreeTestCards(Card winningCard, Card losingCard1, Card losingCard2)
    {
        Assert.AreEqual(_fold.GetWinner().Card, winningCard);
        Assert.AreNotSame(_fold.GetWinner().Card, losingCard1);
        Assert.AreNotSame(_fold.GetWinner().Card, losingCard2);
    }

    /// <summary>
    ///     Simule deux plis où 3 cartes sont jouées, dans tous les sens possibles,
    ///     et teste si la carte gagnante est correcte.
    ///     La carte qui gagne doit toujours être la même, indépendamment de l'ordre des joueurs.
    /// </summary>
    /// <param name="winningCard">La carte qui devrait gagner</param>
    /// <param name="losingCard1">Une carte qui devrait perdre</param>
    /// <param name="losingCard2">L'autre carte qui devrait perdre</param>
    private void PlayThreeTestCardsBotsWays(Card winningCard, Card losingCard1, Card losingCard2)
    {
        // winningCard jouée en premier
        PlayThreeTestCards(winningCard, losingCard1, losingCard2);
        CheckThreeTestCards(winningCard, losingCard1, losingCard2);
        ResetFold();
        PlayThreeTestCards(winningCard, losingCard2, losingCard1);
        CheckThreeTestCards(winningCard, losingCard1, losingCard2);
        ResetFold();

        // winningCard jouée en deuxième
        PlayThreeTestCards(losingCard1, winningCard, losingCard2);
        CheckThreeTestCards(winningCard, losingCard1, losingCard2);
        ResetFold();
        PlayThreeTestCards(losingCard2, winningCard, losingCard1);
        CheckThreeTestCards(winningCard, losingCard1, losingCard2);
        ResetFold();

        // winningCard jouée en dernier
        PlayThreeTestCards(losingCard1, losingCard2, winningCard);
        CheckThreeTestCards(winningCard, losingCard1, losingCard2);
        ResetFold();
        PlayThreeTestCards(losingCard2, losingCard1, winningCard);
        CheckThreeTestCards(winningCard, losingCard1, losingCard2);
    }

    // Test sur les cartes numérotées //

    [Test]
    public void HighestRedCardShouldWinIfTheTurnColorIsRed()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Red);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void HighestBlueCardShouldWinIfTheTurnColorIsBlue()
    {
        var c1 = Card.NumberedCard(13, Colors.Blue);
        var c2 = Card.NumberedCard(1, Colors.Blue);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void HighestYellowCardShouldWinIfTheTurnColorIsYellow()
    {
        var c1 = Card.NumberedCard(13, Colors.Yellow);
        var c2 = Card.NumberedCard(1, Colors.Yellow);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void HighestBlackCardShouldWinIfTheTurnColorIsBlack()
    {
        var c1 = Card.NumberedCard(13, Colors.Black);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    // Test sur l'atout (rouge) //

    [Test]
    public void IfTheTurnColorIsRedBlueCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTwoTestCards(c1, c2, true);
    }

    [Test]
    public void IfTheTurnColorIsRedYellowCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTwoTestCards(c1, c2, true);
    }

    // Test sur l'atout (bleu) //

    [Test]
    public void IfTheTurnColorIsBlueRedCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Blue);
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTwoTestCards(c1, c2, true);
    }

    [Test]
    public void IfTheTurnColorIsBlueYellowCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Blue);
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTwoTestCards(c1, c2, true);
    }

    // Test sur l'atout (jaune) //

    [Test]
    public void IfTheTurnColorIsYellowRedCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Yellow);
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTwoTestCards(c1, c2, true);
    }

    [Test]
    public void IfTheTurnColorIsYellowBlueCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Yellow);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTwoTestCards(c1, c2, true);
    }

    // Test de l'atout (noir) //

    [Test]
    public void IfTheTurnColorIsBlackRedCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTwoTestCards(c1, c2, true);
    }

    [Test]
    public void IfTheTurnColorIsBlackBlueCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTwoTestCards(c1, c2, true);
    }

    [Test]
    public void IfTheTurnColorIsBlackYellowCardsShouldNotWin()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTwoTestCards(c1, c2, true);
    }

    // Test sur les cartes noires //

    [Test]
    public void BlackCardsShouldWinEvenIfTheTurnColorIsRed()
    {
        var c1 = Card.NumberedCard(13, Colors.Red);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTwoTestCards(c1, c2, false);
    }

    [Test]
    public void BlackCardsShouldWinEvenIfTheTurnColorIsBlue()
    {
        var c1 = Card.NumberedCard(13, Colors.Blue);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTwoTestCards(c1, c2, false);
    }

    [Test]
    public void BlackCardsShouldWinEvenIfTheTurnColorIsYellow()
    {
        var c1 = Card.NumberedCard(13, Colors.Yellow);
        var c2 = Card.NumberedCard(1, Colors.Black);

        PlayTwoTestCards(c1, c2, false);
    }

    [Test]
    public void HighestBlackCardShouldWinEvenIfTheTurnColorIsNotBlack()
    {
        var c1 = Card.NumberedCard(5, Colors.Black);
        var c2 = Card.NumberedCard(1, Colors.Black);
        var c3 = Card.NumberedCard(13, Colors.Red);
        
        PlayThreeTestCardsBotsWays(c1, c2, c3);
    }

    // Tests des escapes //

    [Test]
    public void ARedCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Red);
        var c2 = Card.Escape();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ABlueCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Blue);
        var c2 = Card.Escape();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void AYellowCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Yellow);
        var c2 = Card.Escape();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ABlackCardShouldWinAgainstAnEscape()
    {
        var c1 = Card.NumberedCard(1, Colors.Black);
        var c2 = Card.Escape();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    // Test des sirènes //

    [Test]
    public void AMermaidShouldWinAgainstAnEscape()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Escape();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void AMermaidShouldWinAgainstRedCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void AMermaidShouldWinAgainstBlueCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void AMermaidShouldWinAgainstYellowCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void AMermaidShouldWinAgainstBlackCards()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.NumberedCard(13, Colors.Black);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    // Test des pirates //

    [Test]
    public void APirateShouldWinAgainstAnEscape()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Escape();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void APirateShouldWinAgainstRedCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void APirateShouldWinAgainstBlueCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void APirateShouldWinAgainstYellowCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void APirateShouldWinAgainstBlackCards()
    {
        var c1 = Card.Pirate();
        var c2 = Card.NumberedCard(13, Colors.Black);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void APirateShouldWinAgainstAMermaid()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Mermaid();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    // Test du Skull King //

    [Test]
    public void ASkullKingShouldWinAgainstAnEscape()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Escape();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstRedCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Red);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstBlueCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Blue);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstYellowCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Yellow);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstBlackCards()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.NumberedCard(13, Colors.Black);

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void ASkullKingShouldWinAgainstAPirate()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.Pirate();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    // Cas particulier //

    [Test]
    public void AMermaidShouldWinAgainstASkullKing()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.SkullKing();

        PlayTwoTestCardsBothWays(c1, c2);
    }

    [Test]
    public void TheFirstMermaidShouldWinAgainstASkullKing()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Mermaid();
        var c3 = Card.SkullKing();
        
        // Sière 1 gagne
        PlayThreeTestCards(c1, c2, c3);
        CheckThreeTestCards(c1, c2, c3);
        ResetFold();
        PlayThreeTestCards(c1, c3, c2);
        CheckThreeTestCards(c1, c2, c3);
        ResetFold();
        PlayThreeTestCards(c3, c1, c2);
        CheckThreeTestCards(c1, c2, c3);
        ResetFold();

        // Sirène 2 gagne
        PlayThreeTestCards(c2, c1, c3);
        CheckThreeTestCards(c2, c1, c3);
        ResetFold();
        PlayThreeTestCards(c2, c3, c1);
        CheckThreeTestCards(c2, c1, c3);
        ResetFold();
        PlayThreeTestCards(c3, c2, c1);
        CheckThreeTestCards(c2, c1, c3);
    }

    [Test]
    public void AMermaidShouldWinAgainstAPirateIfThereIsASkullKing()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Pirate();
        var c3 = Card.SkullKing();

        PlayThreeTestCardsBotsWays(c1, c2, c3);
    }

    // Tests d'égalités //

    [Test]
    public void TheFirstEscapePlayedShouldWinIfThereAreOnlyEscapes()
    {
        var c1 = Card.Escape();
        var c2 = Card.Escape();

        PlayTwoTestCards(c1, c2, true);
        ResetFold();
        PlayTwoTestCards(c2, c1, true);
    }

    [Test]
    public void TheFirstMermaidPlayedShouldWin()
    {
        var c1 = Card.Mermaid();
        var c2 = Card.Mermaid();

        PlayTwoTestCards(c1, c2, true);
        ResetFold();
        PlayTwoTestCards(c2, c1, true);
    }

    [Test]
    public void TheFirstPiratePlayedShouldWin()
    {
        var c1 = Card.Pirate();
        var c2 = Card.Pirate();

        PlayTwoTestCards(c1, c2, true);
        ResetFold();
        PlayTwoTestCards(c2, c1, true);
    }

    [Test]
    public void TheFirstSkullKingPlayedShouldWin()
    {
        var c1 = Card.SkullKing();
        var c2 = Card.SkullKing();

        PlayTwoTestCards(c1, c2, true);
        ResetFold();
        PlayTwoTestCards(c2, c1, true);
        ;
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