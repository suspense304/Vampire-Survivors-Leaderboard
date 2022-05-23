using System;
using System.Collections.Generic;

#nullable disable

namespace Vampire_Survivors_Leaderboard.Models
{
    public partial class Leaderboard
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public int Version { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual Version VersionNavigation { get; set; }
    }
}
