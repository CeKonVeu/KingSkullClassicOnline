namespace KingSkullClassicOnline.Shared;

public static class Events
{
    // General
    public const string OnError = "OnError";
    public const string GameStarted = "GameStarted";

    // WaitingRoom
    public const string StartGame = "StartGame";
    public const string CreateRoom = "CreateRoom";
    public const string JoinRoom = "JoinRoom";
    public const string RoomChanged = "RoomChanged";

    // GameBoard
    public const string HandChanged = "HandChanged";
    public const string VoteAsked = "VoteAsked";
    public const string MustPlay = "MustPlay";
    public const string SendVote = "SendVote";
    public const string RoundStarted = "RoundStarted";
    public const string RoundEnded = "RoundEnded";
    public const string FoldEnded = "FoldEnded";
    public const string PlayCard = "PlayCard";
    public const string CardPlayed = "CardPlayed";
    public const string GameEnded = "GameEnded";

    //TODO popup
    public const string ScaryMary = "ScaryMary";
}