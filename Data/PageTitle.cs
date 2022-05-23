using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vampire_Survivors_Leaderboard.Data
{
    public class PageTitle
    {
        public string Title { get; set; }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public string GetTitle()
        {
            return Title;
        }
    }
}
