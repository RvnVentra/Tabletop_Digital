using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Capstone
{
    public class GameHub : Hub
    {
        readonly GameManager GM = GameManager.Instance;
        readonly ChatManager CM = ChatManager.Instance;

        public static ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        

        public override Task OnConnectedAsync()
        {
            Debug.Log("User Connected: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //Context.ConnectionId
            GM.RemovePlayer(Context.ConnectionId);
            UpdatePlayerList();
            return base.OnDisconnectedAsync(exception);
        }

        //---------------- Update Methods ------------------

        public async void UpdateTable()
        {
            await Clients.All.SendAsync("UpdateTable", GM.Table);
        }

        public async void UpdateHand()
        {
            await Clients.Caller.SendAsync("UpdateHand", GM.PlayerCards[Context.ConnectionId]);
        }

        public async void UpdateChat()
        {
            await Clients.All.SendAsync("UpdateChat", CM.ChatBox);
        }

        public async void UpdatePlayerList()
        {
            await Clients.All.SendAsync("UpdatePlayerList", GM.Players);
        }

        //---------------- Call Methods ------------------

        public async void EnterName(string name)
        {
            int gameID = Users.Count + 1;

            Users.TryAdd(Context.ConnectionId, new User()
            {
                ConnectionId = Context.ConnectionId,
                Username = name != "" ? name : "Anonymous",
                GameID = gameID
            });

            GM.AddPlayer(Context.ConnectionId, name != "" ? name : "Anonymous");
            await Clients.Caller.SendAsync("JoinGame", gameID);
        }

        public void DrawCard()
        {
            GM.DrawCard(Context.ConnectionId);

            UpdatePlayerList();
            UpdateHand();
        }

        public void PlayCard(int cardID)
        {
            GM.PlayCard(Context.ConnectionId, cardID);
            Debug.Log(cardID + " Was Played");

            UpdatePlayerList();
            UpdateTable();
            UpdateHand();
        }

        public void Chat(string input)
        {
            CM.NewMessage(Users[Context.ConnectionId].Username, input);

            UpdateChat();
        }
    }

    public class User
    {
        public string ConnectionId { get; set; }
        public int GameID { get; set; }
        public string Username { get; set; }
    }
}
