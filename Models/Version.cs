using System;
using System.Collections.Generic;

#nullable disable

namespace Vampire_Survivors_Leaderboard.Models
{
    public partial class Version
    {
        public Version()
        {
            Entries = new HashSet<Entry>();
            Leaderboards = new HashSet<Leaderboard>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Leaderboard> Leaderboards { get; set; }
    }
}
