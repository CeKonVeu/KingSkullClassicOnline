namespace KingSkullClassicOnline.Engine;

using Cards;
using Game;
using Shared;

public interface IView
{
    Task CardPlayed(PlayerData data, string card, string winnerName);
    Task GameEnded(int[] scores, string winner);
    Task GameStarted(IEnumerable<PlayerData> players);
    Task HandReceived(PlayerData player, List<Card> cards);
    Task MustPlay(PlayerData player, IEnumerable<Card> availableCards);
    Task MustVote(int min, int max);
    Task NotifyError(PlayerData player, string message);
    Task PlayerJoined(PlayerData player);
    Task PlayerLeft(PlayerData player);
    Task RoomCreated(string roomName, PlayerData player);
    Task RoundEnded(int[] scores);
    Task RoundStarted(int turn, IEnumerable<PlayerVote> votes);
}