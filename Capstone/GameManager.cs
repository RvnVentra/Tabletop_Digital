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
        public Table Board = new Table();

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
            Board.Hand = new List<Card>();

            for (int i = 0; i < 7; i++)
            {
                Board.Hand.Add(DrawCard());
            }

            Board.TopCard = DrawCard();
        }

        public void ShuffleDeck()
        {
            Deck = Deck.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public Card DrawCard()
        {
            if (Deck.Count <= 0)
                return null;

            Card card = Deck[0];
            Deck.RemoveAt(0);
            return card;
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
    

