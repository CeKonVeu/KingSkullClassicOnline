namespace KingSkullClassicOnline.Engine;

using Card;
using Game;

/// <summary>
///     Gère le déroulement d'une partie
/// </summary>
public class Controller
{
    public int areReady;

    /// <summary>
    ///     Constructeur
    /// </summary>
    public Controller()
    {
        areReady = 0;
        Players = new List<Player>();
        Turn = 1;
        Deck = new List<Card.Card>();
        CreateDeck();
    }

    public Round CurrentRound { get; set; }

    public List<Card.Card> Deck { get; }

    public List<Player> Players { get; }

    public int Turn { get; set; }


    /// <summary>
    ///     ajoute un joueur au controleur
    /// </summary>
    /// <param name="p">joueur à ajouter</param>
    public void AddPlayer(Player p)
    {
        if (Players.Count > Config.MaxPlayer)
            throw new Exception("Max player reached");

        if (Players.Exists(player => player.Id == p.Id))
            throw new Exception("Player with this id already exists");

        Players.Add(p);
    }

    /// <summary>
    ///     Crée un deck de cartes selon les spécificités du fichier config
    /// </summary>
    private void CreateDeck()
    {
        for (var i = 1; i <= Config.NumberNumCards; ++i)
        {
            Deck.Add(new NumberedCard(i, "Red_" + i, Colors.Red));
            Deck.Add(new NumberedCard(i, "Blue_" + i, Colors.Blue));
            Deck.Add(new NumberedCard(i, "Yellow_" + i, Colors.Yellow));
            Deck.Add(new NumberedCard(i, "Black_" + i, Colors.Black));
        }

        for (var i = 0; i < Config.NumberEscapes; ++i)
            Deck.Add(new SpecialCard(Config.EscapeValue, "Escape")); // escape

        for (var i = 0; i < Config.NumberMermaids; ++i)
            Deck.Add(new SpecialCard(Config.MermaidValue, "Mermaid")); // mermaids

        for (var i = 1; i <= Config.NumberPirates; ++i)
            Deck.Add(new SpecialCard(Config.PirateValue, "Pirate_" + i)); // pirates

        for (var i = 0; i < Config.NumberScaryM; ++i)
            Deck.Add(new SpecialCard(Config.PirateValue, "ScaryMary")); // scary Mary

        for (var i = 0; i < Config.NumberSkullKing; ++i)
            Deck.Add(new SpecialCard(Config.SkullKingValue, "SkullKing")); // Skull king
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
    ///     enlève un joueur au controleur
    /// </summary>
    /// <param name="p">joueur à enlever</param>
    public void RemovePlayer(string p)
    {
        // remove the player from the list with the name p
        var player = Players.Find(x => x.Id == p);
        if (player != null)
            Players.Remove(player);
    }

    /// <summary>
    ///     lance une partie et crée la première manche
    /// </summary>
    public void StartGame()
    {
        while (Turn <= Config.TurnNumber)
        {
            CurrentRound = new Round(this);
            CurrentRound.Play();
            Turn++;
        }
    }
}