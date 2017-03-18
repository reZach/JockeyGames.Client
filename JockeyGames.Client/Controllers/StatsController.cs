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
        private MatchesService matchesService = new MatchesService();

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
            List<PlayerDTO> players = await playerService.GetPlayersAsync();
            List<MatchDTO> matches = await matchesService.GetMatchesAsync();

            StatsCompositeViewModel viewmodel = new StatsCompositeViewModel(players, matches);

            viewmodel.PlayerStats.Sort((a, b) => a.MatchScore.CompareTo(b.MatchScore));

            return View(viewmodel.PlayerStats);
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

            List<PlayerDTO> players = await playerService.GetPlayersAsync();
            List<MatchDTO> matches = await matchesService.GetMatchesAsync();

            StatsCompositeViewModel viewmodel = new StatsCompositeViewModel(players, matches);

            return View(viewmodel.GetSinglePlayerById(id.Value));
        }

        // GET: Stats/How
        public ActionResult How()
        {
            return View();
        }
    }
}