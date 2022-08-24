using GameLogic.Card;

namespace GameLogic;

/// <summary>
///     Gère le déroulement d'une partie
/// </summary>
public class Controller
{
    private static readonly Random _random = new();

    /// <summary>
    ///     Constructeur
    /// </summary>
    public Controller()
    {
        Players = new List<Player>();
        Turn = 1;
        Deck = new List<Card.Card>();
        CreateDeck();
    }

    public List<Card.Card> Deck { get; }

    public static List<Player> Players { get; private set; }

    public static int Turn { get; private set; }

    /// <summary>
    ///     Crée un deck de cartes selon les spécificités du fichier config
    /// </summary>
    private void CreateDeck()
    {
        for (var i = 1; i <= Config.NumberNumcards; ++i)
        {
            Deck.Add(new NumberedCard(i, "Red_" + i, Colors.Red));
            Deck.Add(new NumberedCard(i, "Blue_" + i, Colors.Blue));
            Deck.Add(new NumberedCard(i, "Yellow_" + i, Colors.Yellow));
            Deck.Add(new NumberedCard(i, "Black" + i, Colors.Black));
        }

        for (var i = 0; i < Config.NumberEscapes; ++i)
            Deck.Add(new SpecialCard(Config.EscapeValue, "Escape")); // escape

        for (var i = 0; i < Config.NumberMermaids; ++i)
            Deck.Add(new SpecialCard(Config.MermaidValue, "Mermaid")); // mermaids

        for (var i = 0; i < Config.NumberPirates; ++i)
            Deck.Add(new SpecialCard(Config.PirateValue, "Pirate_" + i)); // pirates

        for (var i = 0; i < Config.NumberScaryM; ++i)
            Deck.Add(new SpecialCard(Config.PirateValue, "ScaryMary")); // scary merry

        for (var i = 0; i < Config.NumberSkullking; ++i)
            Deck.Add(new SpecialCard(Config.SkullKingValue, "SkullKing")); // Skull king
    }

    /// <summary>
    ///     Mélange un array utilisant le "fisher yates shuffle"
    /// </summary>
    /// <param name="array">l'array à mélanger</param>
    /// <typeparam name="T">type du contenu de l'array</typeparam>
    /// <returns>l'array mélangé</returns>
    public static T[] Shuffle<T>(T[] array)
    {
        var n = array.Length;
        for (var i = 0; i < n - 1; i++)
        {
            var r = i + _random.Next(n - i);
            (array[r], array[i]) = (array[i], array[r]);
        }

        return array;
    }

    /// <summary>
    ///     fait une copie du deck et distribue les cartes aux joueurs
    /// </summary>
    public void DealCards()
    {
        var temp = new List<Card.Card>();
        temp.AddRange(Shuffle(Deck.ToArray()));

        for (var index = 0; index < Players.Count; index++)
            Players[index].addCards(temp.GetRange(0 + index * Turn, Turn));
    }

    /// <summary>
    ///     ajoute un joueur au controleur
    /// </summary>
    /// <param name="p">joueur à ajouter</param>
    public void AddPlayer(Player p)
    {
        if (Players.Count < Config.MaxPlayer)
            Players.Add(p);
    }

    /// <summary>
    ///     enlève un joueur au controleur
    /// </summary>
    /// <param name="p">joueur à enlever</param>
    public void RemovePlayer(Player p)
    {
        Players.Remove(p);
    }
}