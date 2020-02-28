using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class Lobby
    {
        public Lobby()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public int Admin { get; set; }
        public int User { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Game> Game { get; set; }
    }
}
