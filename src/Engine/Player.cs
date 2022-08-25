namespace KingSkullClassicOnline.Engine;

/// <summary>
///     Classe représentant un joueur.
///     Celui-ci est représenté par un nom et son tableau de score.
///     Une booléen
/// </summary>
public class Player
{
    private readonly Controller _controller;
    private int _selectedCard;
    public bool IsOwner;

    /// <summary>
    ///     constructeur
    /// </summary>
    /// <param name="playerName">nom du joueur</param>
    public Player(string playerName, Controller controller)
    {
        _selectedCard = 0;
        _controller = controller;
        Name = playerName;
        Hand = new List<Card.Card>();
        Votes = new (int, int)[10];
    }

    /// <summary>
    ///     permet de récupérer le nom
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     permet de récupérer les votes
    /// </summary>
    public (int, int)[] Votes { get; }

    /// <summary>
    ///     getter sur la main du joueur
    /// </summary>
    public List<Card.Card> Hand { get; private set; }

    /// <summary>
    ///     vérifie que la carte voulant être jouée respecte les règles
    /// </summary>
    /// <param name="TurnColor">couleur dominante du tour</param>
    /// <returns>la carte peut être jouée ou non</returns>
    private bool CheckIfCardRespectsRule(Colors TurnColor)
    {
        var temp = false;
        var playableCards = new List<int>();
        var hasCardOfColor = false;

        foreach (var card in Hand)
            if (card.Color == TurnColor)
            {
                hasCardOfColor = true;
                break;
            }

        if (!hasCardOfColor)
            for (var i = 0; i < Hand.Count; ++i)
                playableCards.Add(i);
        else
            for (var i = 0; i < Hand.Count; i++)
                if (hasCardOfColor)
                    if (Hand[i].Color == Colors.None || Hand[i].Color == TurnColor)
                        playableCards.Add(i);

        return playableCards.Contains(_selectedCard);
    }

    /// <summary>
    ///     Ajoute des points au score du joueur
    /// </summary>
    /// <param name="vote">vote du tour</param>
    /// <param name="score">score à ajouter</param>
    /// <param name="turnNumber">numéro du tour</param>
    public void AddScore(int vote, int score)
    {
        Votes[_controller.Turn] = (vote, Votes[_controller.Turn].Item2 + score);
    }

    /// <summary>
    ///     ajoute des cartes à la main du joueur
    /// </summary>
    /// <param name="c">liste des cartes a ajouter</param>
    public void AddCards(List<Card.Card> c)
    {
        Hand = c;
    }

    /// <summary>
    ///     envoi la position de la carte sélectionée
    /// </summary>
    public int PlayCard(Colors turnColor)
    {
        while (!CheckIfCardRespectsRule(turnColor)) _selectedCard++;
        //TODO selectcard = carte sélectionée
        var temp = _selectedCard;

        _selectedCard = 0;

        return temp;
    }

    /// <summary>
    ///     récupère le vote du joueur
    /// </summary>
    /// <param name="turn">limite de vote supérieur</param>
    /// <returns>vote effectué</returns>
    public int Vote(int turn)
    {
        // TODO récupérer textbox et redemander vote si le if ne passe pas
        var vote = 2;

        if (vote >= 0 && vote <= turn)
            return vote;

        return -1;
    }
}