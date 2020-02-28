using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class Game
    {
        public Game()
        {
            ChatRecord = new HashSet<ChatRecord>();
            MoveRecord = new HashSet<MoveRecord>();
        }

        public int Id { get; set; }
        public int GameTypeId { get; set; }
        public int Accountid { get; set; }
        public int Lobbyid { get; set; }
        public int Players { get; set; }
        public int TurnOrder { get; set; }

        public virtual Account Account { get; set; }
        public virtual GameType GameType { get; set; }
        public virtual Lobby Lobby { get; set; }
        public virtual ICollection<ChatRecord> ChatRecord { get; set; }
        public virtual ICollection<MoveRecord> MoveRecord { get; set; }
    }
}
