namespace KingSkullClassicOnline.Engine;

using Cards;
using Game;

/// <summary>
///     Gère le déroulement d'une partie
/// </summary>
public class Controller
{
    private readonly Round?[] _rounds;
    private readonly IView _view;

    private bool _hasStarted;

    /// <summary>
    ///     Constructeur
    /// </summary>
    public Controller(IView view, string roomName, string playerId, string playerName)
    {
        _view = view;
        var player = new Player(playerId, playerName);
        Players = new List<Player> { player };
        Turn = 1;
        Deck = CreateDeck();
        _view.RoomCreated(roomName, player.Data);
        _rounds = new Round[Config.RoundsPerGame];
        _hasStarted = false;
    }

    private Round? CurrentRound
    {
        get => _rounds[Turn];
        set => _rounds[Turn] = value;
    }

    public IEnumerable<Card> Deck { get; }

    public List<Player> Players { get; }

    public int Turn { get; private set; }

    /// <summary>
    ///     Crée un deck de cartes selon les spécificités du fichier config
    /// </summary>
    private static IEnumerable<Card> CreateDeck()
    {
        var deck = new List<Card>();

        for (var i = 1; i <= Config.NumberNumCards; ++i)
        {
            deck.Add(new NumberedCard(i, "Red_" + i, Colors.Red));
            deck.Add(new NumberedCard(i, "Blue_" + i, Colors.Blue));
            deck.Add(new NumberedCard(i, "Yellow_" + i, Colors.Yellow));
            deck.Add(new NumberedCard(i, "Black_" + i, Colors.Black));
        }

        for (var i = 0; i < Config.NumberEscapes; ++i)
            deck.Add(new SpecialCard(Config.EscapeValue, "Escape")); // escape

        for (var i = 0; i < Config.NumberMermaids; ++i)
            deck.Add(new SpecialCard(Config.MermaidValue, "Mermaid")); // mermaids

        for (var i = 1; i <= Config.NumberPirates; ++i)
            deck.Add(new SpecialCard(Config.PirateValue, "Pirate_" + i)); // pirates

        for (var i = 0; i < Config.NumberScaryM; ++i)
            deck.Add(new SpecialCard(Config.PirateValue, "ScaryMary")); // scary Mary

        for (var i = 0; i < Config.NumberSkullKing; ++i)
            deck.Add(new SpecialCard(Config.SkullKingValue, "SkullKing")); // Skull king

        return deck;
    }

    /// <summary>
    ///     Ajoute un joueur à la partie.
    /// </summary>
    /// <param name="playerId">l'id du joueur</param>
    /// <param name="name">le nom du joueur</param>
    /// <returns>vrai si l'ajout s'est effectué, faux sinon</returns>
    public void JoinGame(string playerId, string name)
    {
        if (_hasStarted) return;

        var playerData = new PlayerData(playerId, name);
        if (Players.Count >= Config.MaxPlayers || Players.Exists(p => p.Data.Name == playerId))
            _view.NotifyError(playerData, "La partie est complète");

        Players.Add(new Player(playerData));
        _view.PlayerJoined(playerData);
    }

    /// <summary>
    ///     Retire un joueur de la partie
    /// </summary>
    /// <param name="playerId">l'id du joueur</param>
    /// <returns>vrai si la partie contient encore des joueurs, faux sinon</returns>
    public bool LeaveGame(string playerId)
    {
        var player = Players.First(p => p.Data.Id == playerId);
        Players.Remove(player);

        if (Players.Count <= 0) return false;

        _view.PlayerLeft(player.Data);
        return true;
    }

    private void NotifyNextPlayer()
    {
        if (!_hasStarted || CurrentRound == null || CurrentRound.IsOver) return;

        var nextPlayer = CurrentRound.NextPlayer;

        //TODO ajouter la liste de carte jouable
        _view.MustPlay(nextPlayer.Data, nextPlayer.Hand);
    }

    public void PlayCard(string playerId, string card)
    {
        if (!_hasStarted || CurrentRound == null || CurrentRound.IsOver) return;

        var player = Players.Single(p => p.Data.Id == playerId);

        if (CurrentRound.NextPlayer.Data.Id != playerId)
        {
            _view.NotifyError(player.Data, "Ce n'est pas votre tour de jouer");
            return;
        }

        var playedCard = player.Hand.Find(c => c.Name == card);

        if (playedCard == null)
        {
            _view.NotifyError(player.Data, "Vous ne possédez pas cette carte");
            return;
        }

        CurrentRound.Play(player, playedCard);
        _view.HandReceived(player.Data, player.Hand);
        _view.CardPlayed(player.Data, card);

        if (CurrentRound.IsOver)
        {
            //TODO mettre à jour et envoyer les scores
            _view.RoundEnded(new[] { "" });
            ++Turn;
            StartNextRound();
        }
        else
        {
            NotifyNextPlayer();
        }
    }

    public void SetVote(string playerId, int vote)
    {
        if (!_hasStarted || CurrentRound == null || CurrentRound.AreAllVotesIn()) return;

        var player = Players.Single(p => p.Data.Id == playerId);
        CurrentRound.AddVote(player, vote);

        if (!CurrentRound.AreAllVotesIn()) return;

        NotifyNextPlayer();
    }

    public void StartGame()
    {
        if (_hasStarted) return;

        _hasStarted = true;
        _view.GameStarted();

        StartNextRound();
    }

    public void StartNextRound()
    {
        if (!_hasStarted || CurrentRound is { IsOver: false }) return;

        Console.WriteLine("Starting next round");

        CurrentRound = new Round(Turn, Players, Deck);

        foreach (var player in Players)
        {
            CurrentRound.DealCards(player);
            _view.HandReceived(player.Data, player.Hand);
        }

        _view.MustVote(0, Turn);
    }
}