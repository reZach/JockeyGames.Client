using JockeyGames.Models.PingPong;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace JockeyGames.Client.ViewModels
{
    public class AddNewMatchViewModel
    {
        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        public List<SelectListItem> Players { get; set; }
        public List<SelectListItem> Tournaments { get; set; }

        [HiddenInput]
        [Display(Name = "Player 1")]
        public int PlayerId1 { get; set; }
        [HiddenInput]
        [Display(Name = "Player 2")]
        public int PlayerId2 { get; set; }
        [HiddenInput]
        [Display(Name = "Tournament")]
        public int? TournamentId { get; set; }

        [Range(0, 21)]
        public int Game1Player1Score { get; set; }
        [Range(0, 21)]
        public int Game1Player2Score { get; set; }
        [Range(0, 21)]
        public int Game2Player1Score { get; set; }
        [Range(0, 21)]
        public int Game2Player2Score { get; set; }
        [Range(0, 21)]
        public int? Game3Player1Score { get; set; }
        [Range(0, 21)]
        public int? Game3Player2Score { get; set; }
        //public ICollection<Game> Games { get; set; }

        public void LoadPlayersIntoSelectList(List<Player> players)
        {
            Players = new List<SelectListItem>();
            players.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));

            foreach(Player p in players)
            {
                Players.Add(new SelectListItem()
                {
                    Text = p.Name,
                    Value = Convert.ToString(p.Id)
                });
            }
        }

        /*public void LoadTournamentsIntoSelectList(List<Tournament> tournaments)
        {
            Tournaments = new List<SelectListItem>();
            tournaments.Sort((a, b) => string.CompareOrdinal(a.Title, b.Title));

            Tournaments.Add(new SelectListItem() { Text = "", Value = "" });
            foreach(Tournament t in tournaments)
            {
                Tournaments.Add(new SelectListItem()
                {
                    Text = t.Title,
                    Value = Convert.ToString(t.Id)
                });
            }
        }*/
    }
}