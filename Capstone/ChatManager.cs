using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone
{
    public class ChatManager
    {
        private static ChatManager instance = null;
        public static ChatManager Instance { get { return instance ?? (instance = new ChatManager()); } }

        public readonly List<Message> ChatBox = new List<Message>();

        public void NewMessage(string input)
        {
            Debug.Log(input);
            ChatBox.Add(new Message
            {
                Author = "Anonymous",
                Value = input
            });
        }
    }

    public class Message
    {
        public string Author { get; set; }
        public string Value { get; set; }
    }
}
