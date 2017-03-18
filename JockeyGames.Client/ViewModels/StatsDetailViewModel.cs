using JockeyGames.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JockeyGames.Client.ViewModels
{
    public class StatsDetailViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }

        public int MatchScore { get; set; }
        public double GameWinPercentage { get; set; }
        public double MatchWinPercentage { get; set; }
        public int TotalGamesWon { get; set; }
        public int TotalMatchesWon { get; set; }
        public int TotalGamesPlayed { get; set; }
        public int TotalMatchesPlayed { get; set; }

        public List<PlayerDTO> Players { get; set; }
        public List<MatchHistoryViewModel> MatchHistory { get; set; }

        public void LoadStats(List<MatchDTO> stats)
        {
            MatchHistory = new List<MatchHistoryViewModel>();

            MatchScore = 0;
            GameWinPercentage = 0.0;
            MatchWinPercentage = 0.0;
            TotalGamesWon = 0;
            TotalMatchesWon = 0;
            TotalGamesPlayed = 0;
            TotalMatchesPlayed = 0;

            var gamesInMatch = 0;
            var isPlayer1 = false;
            var player1Won = false;
            var g1 = 0;
            var g2 = 0;
            var g3 = 0;

            foreach (MatchDTO m in stats)
            {
                // just for testing
                if (m.G1P1Score == 0 && m.G1P2Score == 0 &&
                    m.G2P1Score == 0 && m.G2P2Score == 0 &&
                    m.G3P1Score == 0 && m.G3P2Score == 0)
                {
                    continue;
                }

                // calculate total games
                if (m.G1P1Score != 0 || m.G1P2Score != 0)
                {
                    gamesInMatch++;
                }
                if (m.G2P1Score != 0 || m.G2P2Score != 0)
                {
                    gamesInMatch++;
                }
                if (m.G3P1Score != 0 || m.G3P2Score != 0)
                {
                    gamesInMatch++;
                }
                TotalGamesPlayed += gamesInMatch;
                TotalMatchesPlayed++;


                // check if player1 or 2
                isPlayer1 = m.PlayerId1 == PlayerId;
                if (isPlayer1)
                {
                    PlayerId = m.PlayerId1;
                }
                else
                {
                    PlayerId = m.PlayerId2;
                }

                // get scores
                g1 = m.G1P1Score - m.G1P2Score;
                g2 = m.G2P1Score - m.G2P2Score;
                g3 = m.G3P1Score - m.G3P2Score;
                if (g3 > 0 || 
                    (g3 == 0 && (g1 > 0 && g2 > 0)))
                {
                    player1Won = true;
                }

                // games won calc
                if (isPlayer1 && player1Won)
                {
                    TotalMatchesWon++;

                    if (g1 > 0)
                    {
                        TotalGamesWon++;
                    }
                    if (g2 > 0)
                    {
                        TotalGamesWon++;
                    }
                    if (g3 > 0)
                    {
                        TotalGamesWon++;
                    }
                }
                else if (!isPlayer1 && !player1Won)
                {
                    TotalMatchesWon++;

                    if (g1 < 0)
                    {
                        TotalGamesWon++;
                    }
                    if (g2 < 0)
                    {
                        TotalGamesWon++;
                    }
                    if (g3 < 0)
                    {
                        TotalGamesWon++;
                    }
                }


                // match history
                var opponentId = m.PlayerId1 == PlayerId ? m.PlayerId2 : m.PlayerId1;
                var opponentName = Players.Where(p => p.Id == opponentId).Select(p => p.Name).First();
                bool iWon = (isPlayer1 && player1Won) || (!isPlayer1 && !player1Won);

                int single = MatchHistory.FindIndex(x => x.OpponentName == opponentName);
                if (single < 0)
                {
                    MatchHistory.Add(new MatchHistoryViewModel
                    {
                        OpponentId = opponentId,
                        OpponentName = opponentName,
                        LossCount = iWon ? 0 : 1,
                        WinCount = iWon ? 1 : 0,
                        WinRate = iWon ? 100 : 0
                    });
                }
                else
                {
                    MatchHistory[single].LossCount = iWon ? MatchHistory[single].LossCount : MatchHistory[single].LossCount + 1;
                    MatchHistory[single].WinCount = iWon ? MatchHistory[single].WinCount + 1 : MatchHistory[single].WinCount;
                    MatchHistory[single].WinRate = (int)(Math.Round(((double)MatchHistory[single].WinCount / (double)(MatchHistory[single].LossCount + MatchHistory[single].WinCount)), 2) * 100);
                }

                gamesInMatch = 0;
                player1Won = false;
            }

            // percentages
            if (TotalGamesPlayed > 0)
            {
                GameWinPercentage = Math.Round(((double)TotalGamesWon / (double)TotalGamesPlayed), 2) * 100;
            }
            if (TotalMatchesPlayed > 0)
            {
                MatchWinPercentage = Math.Round(((double)TotalMatchesWon / (double)TotalMatchesPlayed), 2) * 100;
            }                       

            // sort match history
            MatchHistory.Sort((a, b) => string.CompareOrdinal(a.OpponentName, b.OpponentName));

            SetName();
        }

        private void SetName()
        {
            if (Players.Count > 0)
            {
                foreach (PlayerDTO p in Players)
                {
                    if (PlayerId == p.Id)
                    {
                        Name = p.Name;
                        break;
                    }
                }
            }
        }
    }
}