using JockeyGames.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JockeyGames.Client.ViewModels
{
    public class MatchViewModel : MatchDTO
    {
        public List<SelectListItem> PlayersList1 { get; set; }
        public List<SelectListItem> PlayersList2 { get; set; }

        public PlayerDTO Player1 { get; set; }
        public PlayerDTO Player2 { get; set; }

        public void LoadPlayersIntoSelectList(List<PlayerDTO> playersDTO)
        {
            PlayersList1 = new List<SelectListItem>();
            PlayersList2 = new List<SelectListItem>();
            playersDTO.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));

            foreach (PlayerDTO p in playersDTO)
            {
                PlayersList1.Add(new SelectListItem()
                {
                    Text = p.Name,
                    Value = Convert.ToString(p.Id),
                    Selected = (PlayerId1 != 0 && PlayerId1 == p.Id)
                });
                PlayersList2.Add(new SelectListItem()
                {
                    Text = p.Name,
                    Value = Convert.ToString(p.Id),
                    Selected = (PlayerId2 != 0 && PlayerId2 == p.Id)
                });

                if (PlayerId1 != 0 && PlayerId1 == p.Id)
                {
                    Player1 = p;
                }
                if (PlayerId2 != 0 && PlayerId2 == p.Id)
                {
                    Player2 = p;
                }
            }
        }        
    }
}