using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class GameType
    {
        public GameType()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string GameName { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
