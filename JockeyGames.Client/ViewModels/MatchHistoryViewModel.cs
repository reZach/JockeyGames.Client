using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JockeyGames.Client.ViewModels
{
    public class MatchHistoryViewModel
    {
        public int OpponentId { get; set; }
        public string OpponentName { get; set; }
        public int LossCount { get; set; }
        public int WinCount { get; set; }
        public int WinRate { get; set; }
    }
}