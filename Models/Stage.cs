using System;
using System.Collections.Generic;

#nullable disable

namespace Vampire_Survivors_Leaderboard.Models
{
    public partial class Stage
    {
        public Stage()
        {
            Entries = new HashSet<Entry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }
}
