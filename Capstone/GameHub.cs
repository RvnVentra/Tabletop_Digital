using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace Capstone
{
    public class GameHub : Hub
    {
        private static GameHub instance = null;
        public static GameHub Instance { get { return instance ?? (instance = new GameHub()); } }

        readonly GameManager GM = GameManager.Instance;
        readonly ChatManager CM = ChatManager.Instance;

        public async void UpdateTable()
        {
            await Clients.All.SendAsync("UpdateTable", GM.Table);
        }

        public async void UpdateHand()
        {
            await Clients.Caller.SendAsync("UpdateHand", GM.Table.Hand);
        }

        public async void UpdateChat()
        {
            await Clients.All.SendAsync("UpdateChat", CM.ChatBox);
        }

        public void DrawCard()
        {
            Card card = GM.DrawCard();
            GM.Table.Hand.Add(card);
            Debug.Log("Card " + card.Color + ", " + card.Number + " Was Drawn");

            UpdateHand();
        }

        public void PlayCard(int cardID)
        {
            GM.PlayCard(cardID);

            UpdateTable();
            UpdateHand();
        }

        public void Chat(string input)
        {
            CM.NewMessage(input);
            UpdateChat();
        }

        public async void Send(string input)
        {
            Debug.Log(input);

            await Clients.All.SendAsync("Receive", input);
        }
    }
}
