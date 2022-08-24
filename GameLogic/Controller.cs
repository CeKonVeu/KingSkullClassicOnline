using GameLogic.Card;

namespace GameLogic;

/// <summary>
///     Gère le déroulement d'une partie
/// </summary>
public class Controller
{
    private readonly List<Card.Card> _deck;
    private List<Player> _players;
    private int _turn;

    public List<Card.Card> Deck => _deck;

    public List<Player> Players => _players;

    public int Turn => _turn;

    /// <summary>
    ///     Constructeur
    /// </summary>
    public Controller()
    {
        _players = new List<Player>();
        _turn = 1;
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
            _deck.Add(new NumberedCard(i,"Red_"+i, Colors.Red));
            _deck.Add(new NumberedCard(i,"Blue_"+i, Colors.Blue));
            _deck.Add(new NumberedCard(i,"Yellow_"+i, Colors.Yellow));
            _deck.Add(new NumberedCard(i,"Black"+i, Colors.Black));
        }

        for (var i = 0; i < Config.NumberEscapes; ++i) _deck.Add(new SpecialCard(0, "Escape")); // escape

        for (var i = 0; i < Config.NumberMermaids; ++i) _deck.Add(new SpecialCard(14, "Mermaid")); // mermaids

        for (var i = 0; i < Config.NumberPirates; ++i) _deck.Add(new SpecialCard(15, "Pirate_"+i)); // pirates

        for (var i = 0; i < Config.NumberSkullking; ++i) _deck.Add(new SpecialCard(16, "SkullKing")); // Skull king
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
    public void DealCards()
    {
        List<Card.Card> temp = new List<Card.Card>();
        temp.AddRange(Shuffle(_deck.ToArray()));

        for (var index = 0; index < _players.Count; index++)
        {
            _players[index].addCards(temp.GetRange(0 + index * _turn, _turn));
        }
    }

    /// <summary>
    /// ajoute un joueur au controleur
    /// </summary>
    /// <param name="p">joueur à ajouter</param>
    public void AddPlayer(Player p)
    {
        if(_players.Count < Config.MaxPlayer)
            _players.Add(p);
    }
    
    /// <summary>
    /// enlève un joueur au controleur
    /// </summary>
    /// <param name="p">joueur à enlever</param>
    public void RemovePlayer(Player p)
    {
        _players.Remove(p);
    }
}