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

        static int anonCount = 0;

        public override Task OnConnectedAsync()
        {
            Debug.Log("User Connected: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
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
            if(GM.Players.Count > 0)
                TableStatus(GM.Players[0].Name + "'s Turn");

            await Clients.All.SendAsync("UpdatePlayerList", GM.Players);
        }

        //------------- Status Text Methods --------------

        public async void TableStatus(string text)
        {
            await Clients.All.SendAsync("TableStatus", text);
        }

        public async void HandStatus(string text)
        {
            await Clients.Caller.SendAsync("HandStatus", text);
        }

        //---------------- Call Methods ------------------

        public async void EnterName(string name)
        {
            int playerID = Users.Count + 1;

            string userName;

            if (name != "")
                userName = name;
            else
            {
                userName = anonCount == 0 ? "Anonymous" : "Anonymous(" + anonCount + ")";
                anonCount++;
            }

            Users.TryAdd(Context.ConnectionId, new User()
            {
                ConnectionId = Context.ConnectionId,
                Username = userName,
                PlayerId = playerID
            });

            GM.AddPlayer(playerID, Context.ConnectionId, name != "" ? name : "Anonymous");
            await Clients.Caller.SendAsync("JoinGame", playerID);
        }

        public void DrawCard()
        {
            GM.DrawCard(Context.ConnectionId);

            //TableStatus(Users[Context.ConnectionId].Username + " Drew a Card");

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
        public int PlayerId { get; set; }
        public string Username { get; set; }
    }
}
