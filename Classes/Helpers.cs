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
    }
}
