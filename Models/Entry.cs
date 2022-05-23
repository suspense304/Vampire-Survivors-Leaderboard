using System;
using System.Collections.Generic;

#nullable disable

namespace Vampire_Survivors_Leaderboard.Models
{
    public partial class Entry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CharacterId { get; set; }
        public int StageId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int Level { get; set; }
        public int Kills { get; set; }
        public decimal SurvivedTime { get; set; }
        public int Gold { get; set; }
        public int Version { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public bool Approved { get; set; }
        public bool Deleted { get; set; }
        public bool Limitless { get; set; }

        public virtual Character Character { get; set; }
        public virtual Stage Stage { get; set; }
        public virtual User User { get; set; }
        public virtual Version VersionNavigation { get; set; }
    }
}
