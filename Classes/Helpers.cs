using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vampire_Survivors_Leaderboard.Classes
{
    public class Helpers
    {
        public  string GetOrdinalSuffix(int num)
        {
            string number = num.ToString();
            if (number.EndsWith("11")) return num + "th";
            if (number.EndsWith("12")) return num + "th";
            if (number.EndsWith("13")) return num + "th";
            if (number.EndsWith("1")) return num + "st";
            if (number.EndsWith("2")) return num + "nd";
            if (number.EndsWith("3")) return num + "rd";
            return num + "th";
        }
    }
}
