using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class MoveRecord
    {
        public int Id { get; set; }
        public int Gameid { get; set; }
        public int Accountid { get; set; }
        public string RecordMove { get; set; }

        public virtual Account Account { get; set; }
        public virtual Game Game { get; set; }
    }
}
