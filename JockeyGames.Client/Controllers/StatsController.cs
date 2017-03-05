using JockeyGames.Client.Services;
using JockeyGames.Client.ViewModels;
using JockeyGames.Models.DTOs;
using JockeyGames.Models.PingPong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JockeyGames.Client.Controllers
{
    public class StatsController : Controller
    {
        private PlayersService playerService = new PlayersService();
        private StatsService statsService = new StatsService();

        // GET: Stats
        public async Task<ActionResult> Index()
        {
            StatListViewModel viewmodel = new StatListViewModel();
            viewmodel.Players = await playerService.GetPlayersAsync();

            viewmodel.Players.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));

            return View(viewmodel);
        }

        // GET: Stats/All
        public async Task<ActionResult> All()
        {
            List<StatsDetailViewModel> viewmodel = new List<StatsDetailViewModel>();
            List<PlayerDTO> players = await playerService.GetPlayersAsync();            

            foreach (PlayerDTO p in players)
            {
                List<MatchDTO> stats = await statsService.GetStats(p.Id);
                var single = new StatsDetailViewModel();
                single.Players = players;
                single.PlayerId = p.Id;
                single.LoadStats(stats);

                viewmodel.Add(single);
            }

            viewmodel.Sort((a, b) => a.MatchScore.CompareTo(b.MatchScore));

            return View(viewmodel);
        }

        // GET: Stats/Detail/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<MatchDTO> stats = await statsService.GetStats(id.Value);
            if (stats == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new StatsDetailViewModel();
            viewmodel.Players = await playerService.GetPlayersAsync();
            viewmodel.PlayerId = id.Value;
            viewmodel.LoadStats(stats);            

            return View(viewmodel);
        }
    }
}