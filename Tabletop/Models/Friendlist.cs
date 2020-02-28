using System;
using System.Collections.Generic;

namespace Tabletop.Models
{
    public partial class Friendlist
    {
        public int Accountid { get; set; }
        public int? FriendId { get; set; }

        public virtual Account Account { get; set; }
    }
}
