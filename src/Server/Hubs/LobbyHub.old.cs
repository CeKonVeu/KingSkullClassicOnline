// namespace KingSkullClassicOnline.Server.Hubs;
//
// using System.Collections.Concurrent;
// using Engine;
// using Engine.Game;
// using Microsoft.AspNetCore.SignalR;
//
// /// <summary>
// ///     Hub qui gère les différents lobby de jeu
// /// </summary>
// public class LobbyHubOld : Hub
// {
//     private static readonly ConcurrentDictionary<string, Controller> groups = new();
//
//     /// <summary>
//     ///     Permet de créer un lobby de jeu
//     /// </summary>
//     /// <param name="playerName">Nom du créateur du lobby</param>
//     public async Task CreateRoom(string playerName)
//     {
//         var roomName = CreateRoomName();
//         if (groups.ContainsKey(roomName))
//             //TODO à changer
//             throw new Exception("Room already exists");
//
//         if (!groups.TryAdd(roomName, new Controller()))
//             throw new Exception("Cannot add room");
//
//
//         var task = Groups.AddToGroupAsync(Context.ConnectionId, roomName);
//         groups[roomName].AddPlayer(new Player(playerName, groups[roomName]), Context.ConnectionId);
//         await task;
//         await SendRoomJoined(roomName);
//         Console.WriteLine($"Room {roomName} created");
//     }
//
//     private static string CreateRoomName()
//     {
//         return Guid.NewGuid().ToString("N");
//     }
//
//     /// <summary>
//     ///     Permet de savoir si un lobby existe ou non.
//     /// </summary>
//     /// <param name="lobbyName">Nom du lobby</param>
//     /// <returns>
//     ///     Le nom du lobby si ce dernier existe via la méthode DoesLobbyExist de l'utilisateur, string vide dans le cas
//     ///     contraire
//     /// </returns>
//     public async Task DoesLobbyExist(string lobbyName)
//     {
//         var result = "";
//         if (groups.ContainsKey(lobbyName))
//             result = lobbyName;
//         Clients.Caller.SendAsync("DoesLobbyExist", result);
//     }
//
//     /// <summary>
//     ///     boucle de jeu
//     /// </summary>
//     /// <param name="lobbyName">lobby que gère la boucle de jeu</param>
//     private async Task Game(string lobbyName)
//     {
//         var controller = groups[lobbyName];
//         //while (controller.Turn <= Config.TurnNumber)
//         //{
//         controller.CurrentRound = new Round(controller);
//         controller.Turn = 4;
//         controller.CurrentRound.DealCards();
//         foreach (var player in groups[lobbyName].Players)
//         {
//             var res = string.Join(",", player.Hand.Select(card => card.Name));
//             await Clients.Client(groups[lobbyName].GetConnectionId(player.Id))
//                 .SendAsync("ReceiveStartingHand", res);
//             //Clients.Group(lobbyName).SendAsync("ReceiveStartingHand", res, groups[lobbyName].GetConnectionId(player.Name));
//         }
//
//         await Clients.Group(lobbyName).SendAsync("AskVote");
//
//         // TODO gestion du pli
//         controller.Turn++;
//         //}
//     }
//
//
//     /// <summary>
//     ///     Rejoindre un lobby de jeu.
//     /// </summary>
//     /// <param name="roomName">Nom du lobby à rejoindre</param>
//     /// <param name="playerName">Nom du joueur qui rejoint le lobby</param>
//     /// <returns>Renvoi le nom du lobby s'il existe en appelant la méthode ReceiveLobbyName, sinon ne renvoi rien</returns>
//     public async Task JoinRoom(string roomName, string playerName)
//     {
//         if (!groups.ContainsKey(roomName))
//             throw new Exception("Room doesn't exist");
//         //TODO changer le 6 par le nombre de joueur max
//         if (groups[roomName].Players.Count >= Config.MaxPlayer)
//             throw new Exception("Room is full");
//
//         var task = Groups.AddToGroupAsync(Context.ConnectionId, roomName);
//         groups[roomName].AddPlayer(new Player(playerName, groups[roomName]), Context.ConnectionId);
//         await task;
//         await SendRoomJoined(roomName);
//         Console.WriteLine($"Player {playerName} joined room {roomName}");
//     }
//
//     /// <summary>
//     ///     Quitte le lobby de jeu
//     /// </summary>
//     /// <param name="lobbyName">Nom du lobby à quitter</param>
//     /// <param name="playerName">Nom du joueur</param>
//     public void LeaveGroup(string lobbyName, string playerName)
//     {
//         if (groups.ContainsKey(lobbyName)) groups[lobbyName].RemovePlayer(playerName);
//     }
//
//     /// <summary>
//     ///     Attend que les joueurs soient prets
//     /// </summary>
//     /// <param name="lobbyName">nom du groupe auquel appartient le joueur</param>
//     public async Task ReadyGame(string lobbyName, string playerName)
//     {
//         groups[lobbyName].SetConnectionId(playerName, Context.ConnectionId);
//         if (++groups[lobbyName].areReady == groups[lobbyName].Players.Count)
//             Game(lobbyName);
//     }
//
//     /// <summary>
//     ///     TODO : A supprimer ou faire un chat
//     /// </summary>
//     /// <param name="user"></param>
//     /// <param name="message"></param>
//     /// <param name="group"></param>
//     public async Task SendMessage(string user, string message, string group)
//     {
//         await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
//     }
//
//     private Task SendRoomJoined(string roomName)
//     {
//         return Clients.Caller.SendAsync("RoomJoined",
//             roomName,
//             groups[roomName].Players.Select(p => p.Id));
//     }
// }

