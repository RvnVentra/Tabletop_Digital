using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class ChatRecord
    {
        public int Id { get; set; }
        public int Gameid { get; set; }
        public int Accountid { get; set; }
        public string ChatInput { get; set; }

        public virtual Account Account { get; set; }
        public virtual Game Game { get; set; }
    }
}
