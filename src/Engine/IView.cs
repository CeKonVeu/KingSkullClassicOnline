namespace KingSkullClassicOnline.Engine;

using Cards;
using Game;

public interface IView
{
    Task CardPlayed(PlayerData data, string card, string winnerName);
    Task GameEnded(string[] scores, string winner);
    Task GameStarted();
    Task HandReceived(PlayerData player, List<Card> cards);
    Task MustPlay(PlayerData player, IEnumerable<Card> availableCards);
    Task MustVote(int min, int max);
    Task NotifyError(PlayerData player, string message);
    Task PlayerJoined(PlayerData player);
    Task PlayerLeft(PlayerData player);
    Task RoomCreated(string roomName, PlayerData player);
    Task RoundEnded(int[] scores);
}