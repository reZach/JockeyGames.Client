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
            var scale21points = false;

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

                // calc match score
                if (m.G1P1Score >= 21 | m.G1P2Score >= 21)
                {
                    scale21points = true;
                }

                // get running score
                int runningScore = 0;

                // 11 point scale
                if (!scale21points)
                {
                    if (isPlayer1)
                    {
                        if (g1 > 0)
                        {
                            runningScore += CalculatePointsForWinner(Math.Abs(g1));
                        }
                        else
                        {
                            runningScore += CalculatePointsForLoser(Math.Abs(g1));
                        }
                    }
                    else
                    {
                        if (g1 > 0)
                        {
                            runningScore += CalculatePointsForLoser(Math.Abs(g1));
                        }
                        else
                        {
                            runningScore += CalculatePointsForWinner(Math.Abs(g1));
                        }
                    }
                    if (isPlayer1)
                    {
                        if (g2 > 0)
                        {
                            runningScore += CalculatePointsForWinner(Math.Abs(g2));
                        }
                        else
                        {
                            runningScore += CalculatePointsForLoser(Math.Abs(g2));
                        }
                    }
                    else
                    {
                        if (g2 > 0)
                        {
                            runningScore += CalculatePointsForLoser(Math.Abs(g2));
                        }
                        else
                        {
                            runningScore += CalculatePointsForWinner(Math.Abs(g2));
                        }
                    }
                    if (isPlayer1)
                    {
                        if (g3 > 0)
                        {
                            runningScore += CalculatePointsForWinner(Math.Abs(g3));
                        }
                        else
                        {
                            runningScore += CalculatePointsForLoser(Math.Abs(g3));
                        }
                    }
                    else
                    {
                        if (g3 > 0)
                        {
                            runningScore += CalculatePointsForLoser(Math.Abs(g3));
                        }
                        else
                        {
                            runningScore += CalculatePointsForWinner(Math.Abs(g3));
                        }
                    }

                    // +30 if you won
                    if ((isPlayer1 && player1Won) || (!isPlayer1 && !player1Won))
                    {
                        runningScore += 30;
                    }
                }
                else if (scale21points)
                {
                }


                MatchScore += runningScore;


                gamesInMatch = 0;
            }

            // percentages
            GameWinPercentage = Math.Round(((double)TotalGamesWon / (double)TotalGamesPlayed), 2) * 100;
            MatchWinPercentage = Math.Round(((double)TotalMatchesWon / (double)TotalMatchesPlayed), 2) * 100;

            // match score
            MatchScore = MatchScore / TotalMatchesPlayed;

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

        private int CalculatePointsForWinner(int difference, int scale = 11)
        {
            if (scale == 11)
            {
                switch (difference)
                {
                    case 2:
                        return 17;
                    case 3:
                        return 12;
                    case 4:
                        return 13;
                    case 5:
                        return 17;
                    case 6:
                        return 21;
                    case 7:
                        return 25;
                    case 8:
                        return 28;
                    case 9:
                        return 30;
                    case 10:
                        return 31;
                    case 11:
                        return 32;
                    default:
                        return 0;
                }
            }
            else if (scale == 21)
            {
                return 0;
            }

            return 0;
        }

        private int CalculatePointsForLoser(int difference, int scale = 11)
        {
            if (scale == 11)
            {
                switch (difference)
                {
                    case 2:
                        return 12;
                    case 3:
                        return 11;
                    case 4:
                        return 10;
                    case 5:
                        return 9;
                    case 6:
                        return 8;
                    case 7:
                        return 7;
                    case 8:
                        return 6;
                    case 9:
                        return 5;
                    case 10:
                        return 4;
                    case 11:
                        return 0;
                    default:
                        return 0;
                }
            }
            else if (scale == 21)
            {
                return 0;
            }

            return 0;
        }
    }
}