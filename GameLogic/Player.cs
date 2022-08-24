namespace GameLogic;

/// <summary>
///     Classe représentant un joueur.
///     Celui-ci est représenté par un nom et son tableau de score.
///     Une booléen
/// </summary>
public class Player
{
    public bool IsOwner;

    /// <summary>
    ///     constructeur
    /// </summary>
    /// <param name="playerName">nom du joueur</param>
    public Player(string playerName)
    {
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
    ///     Ajoute des points au score du joueur
    /// </summary>
    /// <param name="vote">vote du tour</param>
    /// <param name="score">score à ajouter</param>
    /// <param name="turnNumber">numéro du tour</param>
    public void AddScore(int vote, int score)
    {
        Votes[Controller.Turn] = (vote, Votes[Controller.Turn].Item2 + score);
    }

    /// <summary>
    ///     ajoute des cartes à la main du joueur
    /// </summary>
    /// <param name="c">liste des cartes a ajouter</param>
    public void addCards(List<Card.Card> c)
    {
        Hand = c;
    }
}