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

        public void LoadStats(List<MatchDTO> stats)
        {
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

                // get scores
                g1 = m.G1P1Score - m.G1P2Score;
                g2 = m.G2P1Score - m.G2P2Score;
                g3 = m.G3P1Score - m.G3P2Score;
                if (g1 + g2 + g3 > 0)
                {
                    player1Won = true;
                }

                // games won calc
                if (isPlayer1 && player1Won)
                {
                    TotalMatchesWon++;
                    MatchScore++;

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
                    MatchScore++;

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

                gamesInMatch = 0;
            }

            // percentages
            GameWinPercentage = Math.Round(((double)TotalGamesWon / (double)TotalGamesPlayed), 2) * 100;
            MatchWinPercentage = Math.Round(((double)TotalMatchesWon / (double)TotalMatchesPlayed), 2) * 100;

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