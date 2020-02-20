using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone
{
    public class GameManager
    {
        private static GameManager instance = null;
        public static GameManager Instance { get { return instance ?? (instance = new GameManager()); } }

        public Dictionary<string, Game> Games = new Dictionary<string, Game>();

        const string CODE_CHARS =  "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        private GameManager()
        {
            Games.Add("GAME_CODE", new Game());
        }

        public string CreateGame()
        {
            char[] stringChars = new char[4];
            Random rand = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = CODE_CHARS[rand.Next(CODE_CHARS.Length)];
            }

            string code = new string(stringChars);
            Games.Add(code, new Game());
            return code;
        }
    }
}


