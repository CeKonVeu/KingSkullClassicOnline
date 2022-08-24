using GameLogic.Card;

namespace GameLogic;

/// <summary>
///     Gère le déroulement d'une partie
/// </summary>
public class Controller
{
    private readonly List<Card.Card> _deck;
    private Player[] _players;
    private int _turn;

    /// <summary>
    ///     Constructeur
    /// </summary>
    public Controller()
    {
        _players = new Player[Config.MaxPlayer];
        _turn = 0;
        _deck = new List<Card.Card>();
        CreateDeck();
    }

    /// <summary>
    ///     Crée un deck de cartes selon les spécificités du fichier config
    /// </summary>
    private void CreateDeck()
    {
        for (var i = 1; i <= Config.NumberNumcards; ++i)
        {
            _deck.Add(new NumberedCard(i, Colors.Red));
            _deck.Add(new NumberedCard(i, Colors.Blue));
            _deck.Add(new NumberedCard(i, Colors.Yellow));
            _deck.Add(new NumberedCard(i, Colors.Black));
        }

        for (var i = 0; i < Config.NumberEscapes; ++i) _deck.Add(new SpecialCard(0)); // escape

        for (var i = 0; i < Config.NumberMermaids; ++i) _deck.Add(new SpecialCard(1)); // mermaids

        for (var i = 0; i < Config.NumberPirates; ++i) _deck.Add(new SpecialCard(2)); // pirates

        for (var i = 0; i < Config.NumberSkullking; ++i) _deck.Add(new SpecialCard(3)); // Skull king
    }

    /// <summary>
    ///     fait une copie du deck et distribue les cartes aux joueurs
    /// </summary>
    private void DealCards()
    {
        var rng = new Random();
        var temp = _deck.OrderBy(a => rng.Next()).ToList();
    }
}