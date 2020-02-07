using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone
{
    public class GameManager
    {
        private static GameManager instance = null;
        public static GameManager Instance { get { return instance ?? (instance = new GameManager()); } }

        readonly Random random = new Random();
        public List<Card> Deck = new List<Card>();
        public Table Table = new Table();

        private GameManager()
        {
            for (int i = 0; i < 4; i++)
            {
                Deck.Add(new Card { Number = 0, Color = i });

                for (int j = 1; j < 10; j++)
                {
                    Deck.Add(new Card { Number = j, Color = i });
                    Deck.Add(new Card { Number = j, Color = i });
                }
            }

            ShuffleDeck();
            Table.Hand = new List<Card>();

            for (int i = 0; i < 7; i++)
            {
                Table.Hand.Add(DrawCard());
            }

            Table.TopCard = DrawCard();
        }

        public void ShuffleDeck()
        {
            Deck = Deck.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public Card DrawCard()
        {
            if (Deck.Count <= 0)
                return new Card { Number = 0, Color = 0 };

            Card card = Deck[0];
            Deck.RemoveAt(0);
            return card;
        }

        public bool PlayCard(int cardID)
        {
            if (Table.Hand[cardID].Color == Table.TopCard.Color || 
                Table.Hand[cardID].Number == Table.TopCard.Number)
            {
                Table.TopCard = Table.Hand[cardID];
                Table.Hand.RemoveAt(cardID);
                return true;
            }

            return false;
        }
    }

    public class Table
    {
        public List<Card> Hand { get; set; }
        public Card TopCard { get; set; }
    }

    public class Card
    {
        public int Number { get; set; }
        public int Color { get; set; }
    }
} 
    

