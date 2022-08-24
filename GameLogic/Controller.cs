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

    static Random _random = new Random();
    
    /// <summary>
    /// Mélange un array utilisant le "fisher yates shuffle"
    /// </summary>
    /// <param name="array">l'array à mélanger</param>
    /// <typeparam name="T">type du contenu de l'array</typeparam>
    /// <returns>l'array mélangé</returns>
    public static T[] Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            int r = i + _random.Next(n - i);
            (array[r], array[i]) = (array[i], array[r]);
        }

        return array;
    }
    
    /// <summary>
    ///     fait une copie du deck et distribue les cartes aux joueurs
    /// </summary>
    private void DealCards()
    {
        List<Card.Card> temp = new List<Card.Card>();
        temp.AddRange(Shuffle(_deck.ToArray()));

        for (var index = 0; index < _players.Length; index++)
        {
            _players[index].addCards(temp.GetRange(0 + index * _turn, _turn));
        }
    }
}