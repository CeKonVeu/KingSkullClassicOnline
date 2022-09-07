using KingSkullClassicOnline.Engine.Cards;
using KingSkullClassicOnline.Engine.Game;

namespace KingSkullClassicOnline.Engine;

public interface IView
{
    Task CardPlayed(PlayerData player, Card card, PlayerData winner);
    Task GameEnded(int[] scores, string winner);
    Task GameStarted(IEnumerable<PlayerData> players);
    Task HandReceived(PlayerData player, List<Card> cards);
    Task MustPlay(PlayerData player, List<Card> cards);
    Task MustVote(int min, int max);
    Task NotifyError(PlayerData player, string message);
    Task PlayerJoined(PlayerData player);
    Task PlayerLeft(PlayerData player);
    Task RoomCreated(string roomName, PlayerData player);
    Task RoundEnded(int turn, IEnumerable<PlayerVote> totalScores);
    Task FoldEnded(int fold, IEnumerable<PlayerVote> votes);
    Task FoldStarted(int fold);
    Task RoundStarted(int turn, IEnumerable<PlayerVote> votes);
}