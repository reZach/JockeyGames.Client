using JockeyGames.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JockeyGames.Client.ViewModels
{
    public class StatsCompositeViewModel
    {
        public List<StatsDetailViewModel> PlayerStats { get; set; }

        public StatsCompositeViewModel(List<PlayerDTO> players, List<MatchDTO> matches)
        {
            PlayerStats = new List<StatsDetailViewModel>();
            List<MatchDTO> stats = new List<MatchDTO>();

            foreach (PlayerDTO p in players)
            {
                // Add matches player has played in
                foreach (MatchDTO m in matches)
                {
                    if (m.PlayerId1 == p.Id || m.PlayerId2 == p.Id)
                    {
                        stats.Add(m);
                    }
                }

                var single = new StatsDetailViewModel();
                single.Players = players;
                single.PlayerId = p.Id;
                single.LoadStats(stats);

                PlayerStats.Add(single);

                stats.Clear();
            }

            // Set match score
            SetMatchScore();
        }

        public void SetMatchScore()
        {
            foreach (var playerStat in PlayerStats)
            {
                // Start out with match win rate; weighted 65%
                var myMatchScore = playerStat.MatchWinPercentage * 0.65;


                double matchHistoryRunningTotal = 0;
                foreach (var matchHistory in playerStat.MatchHistory)
                {
                    if (matchHistory.WinRate == 100) {
                        matchHistoryRunningTotal += 100.0;
                    }
                    else
                    {
                        var opponentMatchWinPercentage = PlayerStats.Find(x => x.PlayerId == matchHistory.OpponentId).MatchWinPercentage;

                        // If you have higher match win rate over opponent
                        if (playerStat.MatchWinPercentage >= opponentMatchWinPercentage)
                        {
                            matchHistoryRunningTotal += opponentMatchWinPercentage;
                        } else
                        {
                            if (playerStat.MatchWinPercentage <= 5)
                            {
                                matchHistoryRunningTotal += opponentMatchWinPercentage - (0.85 * opponentMatchWinPercentage);
                            }
                            else
                            {
                                matchHistoryRunningTotal += opponentMatchWinPercentage - (playerStat.MatchWinPercentage * (opponentMatchWinPercentage / 100.0));
                            }                            
                        }
                    }
                }

                double opponentHistoryScore = 0;

                if (playerStat.MatchHistory.Count > 0)
                {
                    opponentHistoryScore = ((matchHistoryRunningTotal / (double)(playerStat.MatchHistory.Count * 100)) * 100 * 0.35);
                }
                
                PlayerStats.Find(x => x.PlayerId == playerStat.PlayerId).MatchScore = Convert.ToInt32(Math.Ceiling(myMatchScore + opponentHistoryScore));
            }
        }

        public StatsDetailViewModel GetSinglePlayerById(int id)
        {
            return PlayerStats.Find(x => x.PlayerId == id);
        }
    }
}