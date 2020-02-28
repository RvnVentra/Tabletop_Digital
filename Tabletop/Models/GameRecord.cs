using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class GameRecord
    {
        public int Id { get; set; }
        public int Accountid { get; set; }
        public int? WinCount { get; set; }
        public int? LossCount { get; set; }
        public int? GameCount { get; set; }

        public virtual Account Account { get; set; }
    }
}
