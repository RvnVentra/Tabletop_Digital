using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone
{
    public class GameManager
    {
        private static GameManager instance = null;
        public static GameManager Instance { get { return instance ?? (instance = new GameManager()); } }

        readonly Random random = new Random();
        public List<Card> Deck = new List<Card>();
        public Table Table = new Table();

        public List<Player> Players = new List<Player>();
        public Dictionary<string, List<Card>> PlayerCards = new Dictionary<string, List<Card>>();

        private GameManager()
        {
            for (int i = 0; i < 4; i++)
            {
                Deck.Add(new Card { Number = 0, Color = i });

                for (int j = 1; j < 13; j++)
                {
                    Deck.Add(new Card { Number = j, Color = i });
                    Deck.Add(new Card { Number = j, Color = i });
                }
            }

            ShuffleDeck();
            Table.TopCard = DrawCard();
        }

        public void AddPlayer(int playerId, string connectionId, string name)
        {
            PlayerCards[connectionId] = new List<Card>();

            for (int i = 0; i < 7; i++)
            {
                PlayerCards[connectionId].Add(DrawCard());
            }

            Players.Add(new Player
            {
                PlayerId = playerId,
                ConnectionId = connectionId,
                Name = name,
                CardCount = PlayerCards[connectionId].Count
            });
        }

        public void RemovePlayer(string id)
        {
            try
            {
                Players.Remove(Players.Single(p => p.ConnectionId == id));
                PlayerCards.Remove(id);
            }
            catch { }
        }

        public bool CheckTurn(string id)
        {
            return Players.First().ConnectionId == id;
        }

        public void ShuffleDeck()
        {
            Deck = Deck.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public Card DrawCard()
        {
            if (Deck.Count <= 0)
                return GetRandomCard();
            
            Card card = Deck[0];
            Debug.Log("Card " + card.Color + ", " + card.Number + " Was Drawn");
            Deck.RemoveAt(0);
            return card;
        }

        public bool DrawCard(string id)
        {
            if (CheckTurn(id))
            {
                Players.Single(p => p.ConnectionId == id).CardCount++;
                PlayerCards[id].Add(DrawCard());
                return true;
            }

            return false;
        }

        public Card GetRandomCard()
        {
            return new Card
            {
                Number = random.Next(0, 13),
                Color = random.Next(0, 4)
            };
        }

        public bool PlayCard(string playerID, int cardID)
        {
            if (CheckTurn(playerID))
            {
                Player cPlayer = Players.Single(p => p.ConnectionId == playerID);

                if (PlayerCards[playerID][cardID].Color == Table.TopCard.Color ||
                    PlayerCards[playerID][cardID].Number == Table.TopCard.Number)
                {
                    //Played card becomes top card
                    Table.TopCard = PlayerCards[playerID][cardID];
                    PlayerCards[playerID].RemoveAt(cardID);

                    //Update card count
                    cPlayer.CardCount--;

                    //Card effects

                    //Move current player to bottom of the turn order
                    Players.Add(cPlayer);
                    Players.Remove(cPlayer);

                    return true;
                }
            }

            return false;
        }
    }

    public class Player
    {
        public int PlayerId { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public int CardCount { get; set; }
    }

    public class Table
    {
        public Card TopCard { get; set; }
    }

    public class Card
    {
        public int Number { get; set; }
        public int Color { get; set; }
    }
}


