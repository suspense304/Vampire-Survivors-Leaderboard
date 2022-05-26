using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Vampire_Survivors_Leaderboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Vampire_Survivors_Leaderboard.Classes
{
    public class Helpers
    {
		static readonly List<int> pointsAwarded = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
		vswebsiteContext _context = new vswebsiteContext();

        public string GetOrdinalSuffix(int num)
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
		public string GetOrdinalSuffix(int? num)
		{
			if(num.HasValue)
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
			else
			{
				return "N/A";
			}
		}

		public StageRankings GetStageRanking(int stageId, User user)
        {
            List<int> pointsAwarded = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            List<Entry> records = new List<Entry>();
            Dictionary<User, int> playerPoints = new Dictionary<User, int>();
            int StageCount = _context.Stages.ToList().Count;
            int lstCount;
            Stage stage = _context.Stages.Where(w => w.Id == stageId).FirstOrDefault();

            //Sort List By Kills
            Debug.WriteLine(stage.Name + " Kills Leaders");
            records = _context.Entries
            .Include(i => i.User)
            .Where(w => w.StageId == stageId && w.Approved == true && w.Deleted == false)
            .OrderByDescending(o => o.Kills)
            .ToList();

            var killsRecords = _context.Entries
                            .Include(i => i.User)
                            .Where(w => w.StageId == stageId && w.Approved == true && w.Deleted == false)
                            .OrderByDescending(o => o.Kills)
                            .ToList()
                            .GroupBy(g => (g.User))
                            .Select(s => s.First()).ToList();
            lstCount = (killsRecords.Count < 10) ? records.Count : 10;


            if (killsRecords.Count > 0)
            {
                for (int i = 0; i < lstCount; i++)
                {
                    if (killsRecords[i].User != null)
                    {
                        if (playerPoints.ContainsKey(killsRecords[i].User))
                        {
                            playerPoints[killsRecords[i].User] += pointsAwarded[i];
                            Debug.WriteLine(killsRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                        else
                        {
                            playerPoints.Add(killsRecords[i].User, pointsAwarded[i]);
                            Debug.WriteLine(killsRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                    }
                }

                //Sort List By Gold
                Debug.WriteLine(stage.Name + " Gold Leaders");

                var goldRecords = _context.Entries
                            .Include(i => i.User)
                            .Where(w => w.StageId == stageId && w.Approved == true && w.Deleted == false)
                            .OrderByDescending(o => o.Gold)
                            .ToList()
                            .GroupBy(g => (g.User))
                            .Select(s => s.First()).ToList();
                lstCount = (goldRecords.Count < 10) ? goldRecords.Count : 10;

                for (int i = 0; i < lstCount; i++)
                {
                    if (goldRecords[i].User != null)
                    {
                        if (playerPoints.ContainsKey(goldRecords[i].User))
                        {
                            playerPoints[goldRecords[i].User] += pointsAwarded[i];
                            Debug.WriteLine(goldRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                        else
                        {
                            playerPoints.Add(goldRecords[i].User, pointsAwarded[i]);
                            Debug.WriteLine(goldRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                    }
                }

                //Sort List By Level
                Debug.WriteLine(stage.Name + " Level Leaders");
                var levelRecords = _context.Entries
                            .Include(i => i.User)
                            .Where(w => w.StageId == stageId && w.Approved == true && w.Deleted == false)
                            .OrderByDescending(o => o.Level)
                            .ToList()
                            .GroupBy(g => (g.User))
                            .Select(s => s.First()).ToList();
                lstCount = (levelRecords.Count < 10) ? levelRecords.Count : 10;

                for (int i = 0; i < lstCount; i++)
                {
                    if (levelRecords[i].User != null)
                    {
                        if (playerPoints.ContainsKey(levelRecords[i].User))
                        {
                            playerPoints[levelRecords[i].User] += pointsAwarded[i];
                            Debug.WriteLine(levelRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                        else
                        {
                            playerPoints.Add(levelRecords[i].User, pointsAwarded[i]);
                            Debug.WriteLine(levelRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                    }
                }

                //Sort List By Survived
                Debug.WriteLine(stage.Name + " Survived Leaders");
                var timeRecords = _context.Entries
                            .Include(i => i.User)
                            .Where(w => w.StageId == stageId && w.Approved == true && w.Deleted == false)
                            .OrderByDescending(o => o.SurvivedTime)
                            .ToList()
                            .GroupBy(g => (g.User))
                            .Select(s => s.First()).ToList();
                lstCount = (timeRecords.Count < 10) ? timeRecords.Count : 10;

                for (int i = 0; i < lstCount; i++)
                {
                    if (timeRecords[i].User != null)
                    {
                        if (playerPoints.ContainsKey(timeRecords[i].User))
                        {
                            playerPoints[timeRecords[i].User] += pointsAwarded[i];
                            Debug.WriteLine(timeRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                        else
                        {
                            playerPoints.Add(timeRecords[i].User, pointsAwarded[i]);
                            Debug.WriteLine(timeRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                        }
                    }
                }
            }
            List<KeyValuePair<User, int>> PlayerList = playerPoints.ToList();
            PlayerList.Sort((firstPair, nextPair) => firstPair.Value.CompareTo(nextPair.Value));
            List<CompositeStats> lst = new List<CompositeStats>();

            foreach (var player in PlayerList)
            {
                CompositeStats newPlayer = new CompositeStats
                {
                    User = player.Key,
                    Points = player.Value
                };
                lst.Add(newPlayer);
            }
            lst.Reverse();

            int userRanking = lst.FindIndex(i => i.User.Id == user.Id) + 1;
            return new StageRankings { StageName = stage.Name, Ranking = userRanking };
        }

		public StageRankings GetStageRanking(string stage, List<Entry> allStageEntries, User user)
		{
			// Note: We're using allStageEntries as populated by an external source. It assumes you have all the filters setup how ya want.
			// Note: If you want Limitless included, for instance, you'd have to make sure the filter initially used is correct.

			// So basically, we need to figure out the total points for all the users on this stage
			// Then sort users by their total points
			// And then get their index in that sorted list
			// And that corresponds to their rank
			// What about ties?

			int topN = 10;
			var awardedPoints = new Dictionary<User, int>
			{
				// We'll set an initial value for our user so that in the event the user didn't get any ranking
				// we don't need to do yet another ContainsKey check for them.
				{ user, 0 }
			};

			// Ranking are driven by stats that get into the top 10 (the original code only considered the top 10 entries for a (stage, stat) combo.
			// So, when we get best records for a given user, we will discard any record that falls below that benchmark.
			// Each record that remains is then translated into points which are accumulated for the user.

			// TODO: I don't really want to iterate across every single user.
			// TODO: I want to find if the user is in the top 10 for any stat.
			// TODO: So, I should be able to sort my entries, take the top N, look for the user, get the rank, and convert to points.

			var topNKillRecords = allStageEntries.OrderByDescending(entry => entry.Kills).Take(topN).ToList();
			var topNGoldRecords = allStageEntries.OrderByDescending(entry => entry.Gold).Take(topN).ToList();
			var topNLevelRecords = allStageEntries.OrderByDescending(entry => entry.Level).Take(topN).ToList();
			var topNTimeRecords = allStageEntries.OrderByDescending(entry => entry.SurvivedTime).Take(topN).ToList();

			for(int idx = 0; idx < topN; idx++)
			{
				// The index for this actually corresponds to the zero-based rank.
				// So, we can just grab the user and award points based on the index.

				// The base points for a given index is always gonna be the same, regardless of stat consideration.
				var points = pointsAwarded[idx];

				// Our records are already sorted by their rank (index == zero-based rank).
				// Therefore, we're just going through each rank and asking who achieved that rank for that stat.
				var killRecord = topNKillRecords[idx];
				var goldRecord = topNGoldRecords[idx];
				var levelRecord = topNLevelRecords[idx];
				var timeRecord = topNTimeRecords[idx];

				// Now, for those ranked entries, we grab the user and award points to them.
				// We don't care yet about WHO the user is.
				CreateOrUpdate(killRecord.User, points, awardedPoints);
				CreateOrUpdate(goldRecord.User, points, awardedPoints);
				CreateOrUpdate(levelRecord.User, points, awardedPoints);
				CreateOrUpdate(timeRecord.User, points, awardedPoints);
			}


			// Now, sort this dang points dictionary, find OUR user, and grab their points
			var (_, rank) = awardedPoints
				// Remember that Value in this case is total points accumulated by a user, which will then be used to figure out
				// their overall ranking for this stage.
				.OrderByDescending(kvp => kvp.Value)
				// Because this is now sorted, we can once again say that the ordering corresponds to a zero-based ranking.
				// So, let's make it easy on ourselves and include the index right now.
				.Select((kvp, idx) => (kvp, idx))
				// And finally, we want the rank for the user we were interested in in the first place.
				.First(x => x.kvp.Key == user);

			return new StageRankings
			{
				StageName = stage,
				Ranking = rank
			};
		}

        public StageRankings GetTotalRanking(User user)
        {
            List<int> pointsAwarded = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            List<Entry> records = new List<Entry>();
            Dictionary<User, int> playerPoints = new Dictionary<User, int>();
            int StageCount = _context.Stages.ToList().Count;
            int lstCount;


            for(int x = 1; x < StageCount + 1; x++)
            {
                Stage stage = _context.Stages.Where(w => w.Id == x).FirstOrDefault();

                //Sort List By Kills
                Debug.WriteLine(stage.Name + " Kills Leaders");
                records = _context.Entries
                .Include(i => i.User)
                .Where(w => w.StageId == x && w.Approved == true && w.Deleted == false)
                .OrderByDescending(o => o.Kills)
                .ToList();

                var killsRecords = _context.Entries
                                .Include(i => i.User)
                                .Where(w => w.StageId == x && w.Approved == true && w.Deleted == false)
                                .OrderByDescending(o => o.Kills)
                                .ToList()
                                .GroupBy(g => (g.User))
                                .Select(s => s.First()).ToList();
                lstCount = (killsRecords.Count < 10) ? records.Count : 10;


                if (killsRecords.Count > 0)
                {
                    for (int i = 0; i < lstCount; i++)
                    {
                        if (killsRecords[i].User != null)
                        {
                            if (playerPoints.ContainsKey(killsRecords[i].User))
                            {
                                playerPoints[killsRecords[i].User] += pointsAwarded[i];
                                Debug.WriteLine(killsRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                            else
                            {
                                playerPoints.Add(killsRecords[i].User, pointsAwarded[i]);
                                Debug.WriteLine(killsRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                        }
                    }

                    //Sort List By Gold
                    Debug.WriteLine(stage.Name + " Gold Leaders");

                    var goldRecords = _context.Entries
                                .Include(i => i.User)
                                .Where(w => w.StageId == x && w.Approved == true && w.Deleted == false)
                                .OrderByDescending(o => o.Gold)
                                .ToList()
                                .GroupBy(g => (g.User))
                                .Select(s => s.First()).ToList();
                    lstCount = (goldRecords.Count < 10) ? goldRecords.Count : 10;

                    for (int i = 0; i < lstCount; i++)
                    {
                        if (goldRecords[i].User != null)
                        {
                            if (playerPoints.ContainsKey(goldRecords[i].User))
                            {
                                playerPoints[goldRecords[i].User] += pointsAwarded[i];
                                Debug.WriteLine(goldRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                            else
                            {
                                playerPoints.Add(goldRecords[i].User, pointsAwarded[i]);
                                Debug.WriteLine(goldRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                        }
                    }

                    //Sort List By Level
                    Debug.WriteLine(stage.Name + " Level Leaders");
                    var levelRecords = _context.Entries
                                .Include(i => i.User)
                                .Where(w => w.StageId == x && w.Approved == true && w.Deleted == false)
                                .OrderByDescending(o => o.Level)
                                .ToList()
                                .GroupBy(g => (g.User))
                                .Select(s => s.First()).ToList();
                    lstCount = (levelRecords.Count < 10) ? levelRecords.Count : 10;

                    for (int i = 0; i < lstCount; i++)
                    {
                        if (levelRecords[i].User != null)
                        {
                            if (playerPoints.ContainsKey(levelRecords[i].User))
                            {
                                playerPoints[levelRecords[i].User] += pointsAwarded[i];
                                Debug.WriteLine(levelRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                            else
                            {
                                playerPoints.Add(levelRecords[i].User, pointsAwarded[i]);
                                Debug.WriteLine(levelRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                        }
                    }

                    //Sort List By Survived
                    Debug.WriteLine(stage.Name + " Survived Leaders");
                    var timeRecords = _context.Entries
                                .Include(i => i.User)
                                .Where(w => w.StageId == x && w.Approved == true && w.Deleted == false)
                                .OrderByDescending(o => o.SurvivedTime)
                                .ToList()
                                .GroupBy(g => (g.User))
                                .Select(s => s.First()).ToList();
                    lstCount = (timeRecords.Count < 10) ? timeRecords.Count : 10;

                    for (int i = 0; i < lstCount; i++)
                    {
                        if (timeRecords[i].User != null)
                        {
                            if (playerPoints.ContainsKey(timeRecords[i].User))
                            {
                                playerPoints[timeRecords[i].User] += pointsAwarded[i];
                                Debug.WriteLine(timeRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                            else
                            {
                                playerPoints.Add(timeRecords[i].User, pointsAwarded[i]);
                                Debug.WriteLine(timeRecords[i].User.Name + " + " + pointsAwarded[i].ToString());
                            }
                        }
                    }
                }
            }
            
            List<KeyValuePair<User, int>> PlayerList = playerPoints.ToList();
            PlayerList.Sort((firstPair, nextPair) => firstPair.Value.CompareTo(nextPair.Value));
            List<CompositeStats> lst = new List<CompositeStats>();

            foreach (var player in PlayerList)
            {
                CompositeStats newPlayer = new CompositeStats
                {
                    User = player.Key,
                    Points = player.Value
                };
                lst.Add(newPlayer);
            }
            lst.Reverse();

            int userRanking = lst.FindIndex(i => i.User.Id == user.Id) + 1;
            return new StageRankings { StageName = "Overall", Ranking = userRanking };
        }

		public StageRankings GetOverallRanking(User user, List<Entry> allEntries, List<Stage> stages)
		{
			// Basically, we do everything we do in GetStageRanking, but we do for all given stages and keep accumulating points.
			// This also probably means there is opportunity for more reuse in here than I have put in
			// since there is common logic between the two functions.

			// For each stage
			//	Get the top 10 for that stage
			// Award points to the users in those sets of entries
			// Order by points
			// Select rank

			int topN = 10;
			var awardedPoints = new Dictionary<User, int>
			{
				// We'll set an initial value for our user so that in the event the user didn't get any ranking
				// we don't need to do yet another ContainsKey check for them.
				{ user, 0 }
			};

			foreach (var stage in stages)
			{
				var stageEntries = allEntries.Where(entry => entry.StageId == stage.Id).ToList();

				var topNKillRecords = stageEntries.OrderByDescending(entry => entry.Kills).Take(topN).ToList();
				var topNGoldRecords = stageEntries.OrderByDescending(entry => entry.Gold).Take(topN).ToList();
				var topNLevelRecords = stageEntries.OrderByDescending(entry => entry.Level).Take(topN).ToList();
				var topNTimeRecords = stageEntries.OrderByDescending(entry => entry.SurvivedTime).Take(topN).ToList();

				for (int idx = 0; idx < topN; idx++)
				{
					var points = pointsAwarded[idx];

					var killRecord = topNKillRecords[idx];
					var goldRecord = topNGoldRecords[idx];
					var levelRecord = topNLevelRecords[idx];
					var timeRecord = topNTimeRecords[idx];

					CreateOrUpdate(killRecord.User, points, awardedPoints);
					CreateOrUpdate(goldRecord.User, points, awardedPoints);
					CreateOrUpdate(levelRecord.User, points, awardedPoints);
					CreateOrUpdate(timeRecord.User, points, awardedPoints);
				}
			}

			// Now, sort this dang points dictionary, find OUR user, and grab their points
			var (_, rank) = awardedPoints
				// Remember that Value in this case is total points accumulated by a user, which will then be used to figure out
				// their overall ranking for this stage.
				.OrderByDescending(kvp => kvp.Value)
				// Because this is now sorted, we can once again say that the ordering corresponds to a zero-based ranking.
				// So, let's make it easy on ourselves and include the index right now.
				.Select((kvp, idx) => (kvp, idx))
				// And finally, we want the rank for the user we were interested in in the first place.
				.First(x => x.kvp.Key == user);

			return new StageRankings 
			{ 
				StageName = "Overall",
				Ranking = rank
			};
		}



		void CreateOrUpdate(User user, int incrementPoints, Dictionary<User, int> playerPoints)
		{
			if(playerPoints.ContainsKey(user))
			{
				playerPoints[user] += incrementPoints;
			}
			else
			{
				playerPoints.Add(user, incrementPoints);
			}
		}
	}
}
