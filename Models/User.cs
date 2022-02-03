using System;
using System.Collections.Generic;

#nullable disable

namespace Vampire_Survivors_Leaderboard.Models
{
    public partial class User
    {
        public User()
        {
            Entries = new HashSet<Entry>();
        }

        public int Id { get; set; }
        public string UserKey { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DiscordCode { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }
}
