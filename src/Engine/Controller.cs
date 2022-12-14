namespace KingSkullClassicOnline.Engine;

using Cards;
using Game;
using Shared;

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
        get => _rounds[Turn - 1];
        set => _rounds[Turn - 1] = value;
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
            deck.Add(Card.NumberedCard(i, Color.Red));
            deck.Add(Card.NumberedCard(i, Color.Blue));
            deck.Add(Card.NumberedCard(i, Color.Yellow));
            deck.Add(Card.NumberedCard(i, Color.Black));
        }

        for (var i = 0; i < Config.NumberSkullKing; ++i)
            deck.Add(Card.SkullKing());

        for (var i = 1; i <= Config.NumberPirates; ++i)
            deck.Add(Card.Pirate(i % Config.PirateVariants + 1));

        for (var i = 0; i < Config.NumberScaryM; ++i)
            deck.Add(Card.ScaryMary());

        for (var i = 0; i < Config.NumberMermaids; ++i)
            deck.Add(Card.Mermaid());

        for (var i = 0; i < Config.NumberEscapes; ++i)
            deck.Add(Card.Escape());

        return deck;
    }

    private int[] GetScores(int turn)
    {
        var scores = new int[Players.Count];
        for (var i = 0; i < Players.Count; ++i) scores[i] = Players[i].GetVote(turn)!.Total;
        return scores;
    }

    /// <summary>
    ///     Ajoute un joueur à la partie.
    /// </summary>
    /// <param name="playerId">l'id du joueur</param>
    /// <param name="name">le nom du joueur</param>
    /// <returns>vrai si l'ajout s'est effectué, faux sinon</returns>
    public bool JoinGame(string playerId, string name)
    {
        var playerData = new PlayerData(playerId, name);
        if (_hasStarted)
        {
            _view.NotifyError(playerData, "La partie a déjà commencé");
            return false;
        }

        if (Players.Count >= Config.MaxPlayers || Players.Exists(p => p.Data.Name == playerId))
        {
            _view.NotifyError(playerData, "La partie est complète");
            return false;
        }

        Players.Add(new Player(playerData));
        _view.PlayerJoined(playerData);
        return true;
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

        if (CurrentRound.CurrentColor != Color.None &&
            nextPlayer.Hand.Exists(card => card.Color == CurrentRound.CurrentColor))
            foreach (var card in nextPlayer.Hand)
                card.IsPlayable = card.Color == CurrentRound.CurrentColor
                                  || card.IsSpecial();
        else
            foreach (var card in nextPlayer.Hand)
                card.IsPlayable = true;

        _view.MustPlay(nextPlayer.Data, nextPlayer.Hand);
    }

    public void PlayCard(string playerId, string card)
    {
        if (!_hasStarted || CurrentRound == null || CurrentRound.IsOver || !CurrentRound.AreAllVotesIn()) return;

        var player = Players.Find(p => p.Data.Id == playerId);

        if (player == null)
            return;

        if (CurrentRound.NextPlayer.Data.Id != playerId) return;

        var playedCard = player.Hand.Find(c => card.StartsWith(c.Name));

        if (playedCard == null) return;

        if (playedCard.IsScaryMary())
        {
            var sm = Card.ScaryMary(card == CardNames.ScaryMaryPirate);

            var i = player.Hand.IndexOf(playedCard);
            player.Hand[i] = sm;
            playedCard = sm;
        }

        CurrentRound.Play(player, playedCard);
        foreach (var c in player.Hand) c.IsPlayable = false;
        _view.HandReceived(player.Data, player.Hand);
        _view.CardPlayed(player.Data, playedCard, CurrentRound.CurrentFold.GetWinner().Player.Data);
        var foldNumber = CurrentRound.EndFold();
        if (foldNumber != -1)
            _view.FoldEnded(foldNumber, Players.Select(p => new PlayerVote(p.Data.Id, p.GetVote(Turn)!.Actual)));

        if (CurrentRound.IsOver)
        {
            CurrentRound.EndRound();
            _view.RoundEnded(Turn, Players.Select(p => new PlayerVote(p.Data.Id, p.GetVote(Turn)!.Total)),
                Players[Turn % Players.Count].Data);

            if (Turn == Config.RoundsPerGame)
            {
                _view.GameEnded();
                return;
            }

            ++Turn;
            StartNextRound();
        }
        else
        {
            if (CurrentRound.IsNewFold())
                _view.FoldStarted(CurrentRound.FoldNumber());
            NotifyNextPlayer();
        }
    }

    public void SetVote(string playerId, int vote)
    {
        if (!_hasStarted || CurrentRound == null || CurrentRound.AreAllVotesIn()) return;

        var player = Players.Single(p => p.Data.Id == playerId);
        CurrentRound.AddVote(player, vote);

        if (!CurrentRound.AreAllVotesIn()) return;
        var players = CurrentRound.GetPlayersFromStarting();
        _view.RoundStarted(Turn, players.Select(p => new PlayerVote(p.Data.Id, p.GetVote(Turn)!.Voted)));
        _view.FoldStarted(1);
        NotifyNextPlayer();
    }

    public void StartGame()
    {
        if (_hasStarted || Players.Count is < Config.MinPlayers or > Config.MaxPlayers) return;

        _hasStarted = true;
        _view.GameStarted(Players.Select(p => p.Data));

        StartNextRound();
    }

    private void StartNextRound()
    {
        if (!_hasStarted || CurrentRound is { IsOver: false }) return;

        Console.WriteLine("Starting next round");

        CurrentRound = new Round(Turn, Players, Deck);
        foreach (var player in Players)
        {
            CurrentRound.DealCards(player);
            foreach (var c in player.Hand) c.IsPlayable = false;
            _view.HandReceived(player.Data, player.Hand);
        }

        _view.MustVote(0, Turn);
    }
}