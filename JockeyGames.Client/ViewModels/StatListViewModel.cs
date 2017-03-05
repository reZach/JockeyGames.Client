using JockeyGames.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JockeyGames.Client.ViewModels
{
    public class StatListViewModel
    {
        public List<PlayerDTO> Players { get; set; }
    }
}