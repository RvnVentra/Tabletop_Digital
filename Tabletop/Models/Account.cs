using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class Account
    {
        public Account()
        {
            ChatRecord = new HashSet<ChatRecord>();
            Friendlist = new HashSet<Friendlist>();
            Game = new HashSet<Game>();
            GameRecord = new HashSet<GameRecord>();
            Lobby = new HashSet<Lobby>();
            MoveRecord = new HashSet<MoveRecord>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<ChatRecord> ChatRecord { get; set; }
        public virtual ICollection<Friendlist> Friendlist { get; set; }
        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<GameRecord> GameRecord { get; set; }
        public virtual ICollection<Lobby> Lobby { get; set; }
        public virtual ICollection<MoveRecord> MoveRecord { get; set; }
    }
}
