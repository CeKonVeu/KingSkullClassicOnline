using KingSkullClassicOnline.Engine.Card;
using KingSkullClassicOnline.Engine.Game;

namespace KingSkullClassicOnline.Engine;

/// <summary>
///     Gère le déroulement d'une partie
/// </summary>
public class Controller
{
    private Round _currentRound;

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

    public List<Player> Players { get; }

    public int Turn { get; set; }

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

    /// <summary>
    ///     lance une partie et crée la première manche
    /// </summary>
    public void StartGame()
    {
        while (Turn <= Config.TurnNumber)
        {
            _currentRound = new Round(Players.Count, Turn, this);
            _currentRound.Play();
            Turn++;
        }
    }
}