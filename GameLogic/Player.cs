﻿namespace GameLogic;

/// <summary>
///     Classe représentant un joueur.
///     Celui-ci est représenté par un nom et son tableau de score.
///     Une booléen
/// </summary>
public class Player
{
    public bool IsOwner;
    private List<Card.Card> _hand;

    /// <summary>
    /// constructeur
    /// </summary>
    /// <param name="playerName">nom du joueur</param>
    public Player(string playerName)
    {
        Name = playerName;
        Votes = new (int, int, int)[10];
    }

    /// <summary>
    /// permet de récupérer le nom
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// permet de récupérer les votes
    /// </summary>
    public (int, int, int)[] Votes { get; }

    /// <summary>
    /// Ajoute des points au score du joueur
    /// </summary>
    /// <param name="vote">vote du tour</param>
    /// <param name="result">résultats du tour</param>
    /// <param name="score">score à ajouter</param>
    /// <param name="turnNumber">numéro du tour</param>
    public void addScore(int vote, int result, int score, int turnNumber)
    {
        Votes[turnNumber] = (vote, result, score);
    }

    public void addCards(List<Card.Card> c)
    {
        _hand = c;
    }
}