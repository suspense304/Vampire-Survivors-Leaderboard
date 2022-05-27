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

			int topN = 10;
			var awardedPoints = new Dictionary<User, int>
			{
				// We'll set an initial value for our user so that in the event the user didn't get any ranking
				// we don't need to do yet another ContainsKey check for them.
				{ user, 0 }
			};

			var topNKillsRecords = GetTopRanks(entry => entry.Kills, topN, allStageEntries).Take(topN).ToList();
			var topNGoldRecords = GetTopRanks(entry => entry.Gold, topN, allStageEntries).Take(topN).ToList();
			var topNLevelRecords = GetTopRanks(entry => entry.Level, topN, allStageEntries).Take(topN).ToList();
			var topNTimeRecords = GetTopRanks(entry => entry.SurvivedTime, topN, allStageEntries).Take(topN).ToList();

			AddTally(topNKillsRecords, awardedPoints);
			AddTally(topNGoldRecords, awardedPoints);
			AddTally(topNLevelRecords, awardedPoints);
			AddTally(topNTimeRecords, awardedPoints);

			// Now, sort this points dictionary, find OUR user, and grab their points
			var (_, zeroBasedRank) = awardedPoints
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
				Ranking = zeroBasedRank + 1
			};
		}

		// TODO: This is a funky function in that it seems like it's solely focused on the user, but it's actually giving us information
		// TODO: about the user AND the other users in the topN.
		List<Entry> GetTopRanks<T>(Func<Entry, T> statSelector, int topN, List<Entry> allStageEntries)
		{
			Func<Entry, int?> keySelector = entry => entry?.UserId;
			Func<Entry, Entry, Func<Entry, T>, int> comparer = (entry1, entry2, statSelector) =>
			{
				if(entry1 == null)
				{
					return -1;
				}
				else if(entry2 == null)
				{
					return 1;
				}

				var comparer = Comparer<T>.Default;
				var stat1 = statSelector(entry1);
				var stat2 = statSelector(entry2);
				return comparer.Compare(stat1, stat2);
			};
			var exclusionSorter = new ExclusionSort<Entry, int?, T>(keySelector, comparer);
			var topRanks = exclusionSorter.Sort(allStageEntries, statSelector);

			return topRanks;
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
			// Basically, we do everything we do in GetStageRanking, but we do it for all given stages and keep accumulating points.
			// This also probably means there is opportunity for more reuse in here than I have put in
			// since there is common logic between the two functions.

			int topN = 10;
			var awardedPoints = new Dictionary<User, int>
			{
				// We'll set an initial value for our user so that in the event the user didn't get any ranking
				// we don't need to do yet another ContainsKey check for them.
				{ user, 0 }
			};

			foreach (var stage in stages)
			{
				var allStageEntries = allEntries.Where(entry => entry.StageId == stage.Id).ToList();

				var topNKillsRecords = GetTopRanks(entry => entry.Kills, topN, allStageEntries).Take(topN).ToList();
				var topNGoldRecords = GetTopRanks(entry => entry.Gold, topN, allStageEntries).Take(topN).ToList();
				var topNLevelRecords = GetTopRanks(entry => entry.Level, topN, allStageEntries).Take(topN).ToList();
				var topNTimeRecords = GetTopRanks(entry => entry.SurvivedTime, topN, allStageEntries).Take(topN).ToList();

				AddTally(topNKillsRecords, awardedPoints);
				AddTally(topNGoldRecords, awardedPoints);
				AddTally(topNLevelRecords, awardedPoints);
				AddTally(topNTimeRecords, awardedPoints);
			}

			// Now, sort this points dictionary, find OUR user, and grab their points
			var (_, zeroBasedRank) = awardedPoints
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
				Ranking = zeroBasedRank + 1
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

		/// <summary>
		/// Handles adding points for a user based on their rank within the set of ordered entries.
		/// </summary>
		/// <param name="entries">A list of entries whose length is already clamped to the desired amount, such as the topN relevant entries.</param>
		/// <param name="playerPoints">A dictionary containing a user key and the points accumulate for that user.</param>
		void AddTally(List<Entry> entries, Dictionary<User, int> playerPoints)
		{
			foreach (var (entry, idx) in entries.Select((entry, idx) => (entry, idx)))
			{
				var user = entry.User;
				if (user != null)
				{
					if (playerPoints.ContainsKey(user))
					{
						playerPoints[user] += pointsAwarded[idx];
					}
					else
					{
						playerPoints.Add(user, pointsAwarded[idx]);
					}

					//Debug.WriteLine(user.Name + " + " + pointsAwarded[idx].ToString());
				}
				else
				{
					Debug.WriteLine("How the fuck did this happen?");
				}
			}
		}
	}
}
