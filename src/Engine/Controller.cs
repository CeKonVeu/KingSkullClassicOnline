using KingSkullClassicOnline.Engine.Card;
using KingSkullClassicOnline.Engine.Game;

namespace KingSkullClassicOnline.Engine;

/// <summary>
///     Gère le déroulement d'une partie
/// </summary>
public class Controller
{
    public int areReady;
    public Dictionary<string, string?> playerId;

    /// <summary>
    ///     Constructeur
    /// </summary>
    public Controller()
    {
        areReady = 0;
        playerId = new Dictionary<string, string?>();
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
    ///     ajoute un joueur au controleur
    /// </summary>
    /// <param name="p">joueur à ajouter</param>
    /// <param name="connetionId">id de connexion du joueur</param>
    public void AddPlayer(Player p, string? connetionId)
    {
        if (Players.Count < Config.MaxPlayer)
            Players.Add(p);
        playerId.Add(p.Name, connetionId);
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
        var player = Players.Find(x => x.Name == p);
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

    /// <summary>
    ///     récupère l'id de connexion d'un joueur
    /// </summary>
    /// <param name="playerName">nom du joueur</param>
    /// <returns>son id</returns>
    public string? GetConnectionId(string playerName)
    {
        return playerId[playerName];
    }

    /// <summary>
    ///     set un nouvel id de connection au joueur
    /// </summary>
    /// <param name="playerName">nom du joueur a changer</param>
    /// <param name="connec">nouvel id</param>
    public void SetConnectionId(string playerName, string connec)
    {
        playerId[playerName] = connec;
    }
}