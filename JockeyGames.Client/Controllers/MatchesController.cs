using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JockeyGames.Client.Models;
using JockeyGames.Models.DTOs;
using JockeyGames.Client.Services;
using JockeyGames.Client.ViewModels;

namespace JockeyGames.Client.Controllers
{
    public class MatchesController : Controller
    {
        private MatchesService service = new MatchesService();
        private PlayersService playerService = new PlayersService();

        // GET: Matches
        public async Task<ActionResult> Index()
        {
            List<MatchDTO> matchesDTO = await service.GetMatchesAsync();
            List<MatchViewModel> viewmodels = new List<MatchViewModel>();
            var players = await playerService.GetPlayersAsync();

            foreach (MatchDTO dto in matchesDTO)
            {
                MatchViewModel single = new MatchViewModel
                {
                    Id = dto.Id,
                    DateTime = dto.DateTime,
                    G1P1Score = dto.G1P1Score,
                    G1P2Score = dto.G1P2Score,
                    G2P1Score = dto.G2P1Score,
                    G2P2Score = dto.G2P2Score,
                    G3P1Score = dto.G3P1Score,
                    G3P2Score = dto.G3P2Score,
                    PlayerId1 = dto.PlayerId1,
                    PlayerId2 = dto.PlayerId2
                };
                single.LoadPlayersIntoSelectList(players);

                viewmodels.Add(single);
            }

            return View(viewmodels);
        }

        // GET: Matches/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchDTO matchDTO = await service.GetMatch(id.Value);
            if (matchDTO == null)
            {
                return HttpNotFound();
            }

            MatchViewModel viewmodel = new MatchViewModel
            {
                Id = matchDTO.Id,
                DateTime = matchDTO.DateTime,
                G1P1Score = matchDTO.G1P1Score,
                G1P2Score = matchDTO.G1P2Score,
                G2P1Score = matchDTO.G2P1Score,
                G2P2Score = matchDTO.G2P2Score,
                G3P1Score = matchDTO.G3P1Score,
                G3P2Score = matchDTO.G3P2Score,
                PlayerId1 = matchDTO.PlayerId1,
                PlayerId2 = matchDTO.PlayerId2
            };
            var players = await playerService.GetPlayersAsync();
            viewmodel.LoadPlayersIntoSelectList(players);

            return View(viewmodel);
        }

        // GET: Matches/Create
        public async Task<ActionResult> Create()
        {
            var viewmodel = new MatchViewModel();
            var players = await playerService.GetPlayersAsync();
            viewmodel.DateTime = DateTime.Now;            
            viewmodel.LoadPlayersIntoSelectList(players);

            return View(viewmodel);
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateTime,G1P1Score,G1P2Score,G2P1Score,G2P2Score,G3P1Score,G3P2Score,PlayerId1,PlayerId2")] MatchViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                MatchDTO matchDTO = new MatchDTO
                {
                    Id = viewmodel.Id,
                    DateTime = viewmodel.DateTime,
                    G1P1Score = viewmodel.G1P1Score,
                    G1P2Score = viewmodel.G1P2Score,
                    G2P1Score = viewmodel.G2P1Score,
                    G2P2Score = viewmodel.G2P2Score,
                    G3P1Score = viewmodel.G3P1Score,
                    G3P2Score = viewmodel.G3P2Score,
                    PlayerId1 = viewmodel.PlayerId1,
                    PlayerId2 = viewmodel.PlayerId2
                };

                await service.AddMatch(matchDTO);
                return RedirectToAction("Index");
            }

            return View(viewmodel);
        }

        // GET: Matches/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchDTO matchDTO = await service.GetMatch(id.Value);            

            if (matchDTO == null)
            {
                return HttpNotFound();
            }

            MatchViewModel viewmodel = new MatchViewModel
            {
                Id = matchDTO.Id,
                DateTime = matchDTO.DateTime,
                G1P1Score = matchDTO.G1P1Score,
                G1P2Score = matchDTO.G1P2Score,
                G2P1Score = matchDTO.G2P1Score,
                G2P2Score = matchDTO.G2P2Score,
                G3P1Score = matchDTO.G3P1Score,
                G3P2Score = matchDTO.G3P2Score,
                PlayerId1 = matchDTO.PlayerId1,
                PlayerId2 = matchDTO.PlayerId2
            };
            var players = await playerService.GetPlayersAsync();            
            viewmodel.LoadPlayersIntoSelectList(players);

            return View(viewmodel);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateTime,G1P1Score,G1P2Score,G2P1Score,G2P2Score,G3P1Score,G3P2Score,PlayerId1,PlayerId2")] MatchViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                MatchDTO matchDTO = new MatchDTO
                {
                    Id = viewmodel.Id,
                    DateTime = viewmodel.DateTime,
                    G1P1Score = viewmodel.G1P1Score,
                    G1P2Score = viewmodel.G1P2Score,
                    G2P1Score = viewmodel.G2P1Score,
                    G2P2Score = viewmodel.G2P2Score,
                    G3P1Score = viewmodel.G3P1Score,
                    G3P2Score = viewmodel.G3P2Score,
                    PlayerId1 = viewmodel.PlayerId1,
                    PlayerId2 = viewmodel.PlayerId2
                };

                await service.UpdateMatch(matchDTO);
                return RedirectToAction("Index");
            }
            return View(viewmodel);
        }

        // GET: Matches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchDTO matchDTO = await service.GetMatch(id.Value);
            if (matchDTO == null)
            {
                return HttpNotFound();
            }

            MatchViewModel viewmodel = new MatchViewModel
            {
                Id = matchDTO.Id,
                DateTime = matchDTO.DateTime,
                G1P1Score = matchDTO.G1P1Score,
                G1P2Score = matchDTO.G1P2Score,
                G2P1Score = matchDTO.G2P1Score,
                G2P2Score = matchDTO.G2P2Score,
                G3P1Score = matchDTO.G3P1Score,
                G3P2Score = matchDTO.G3P2Score,
                PlayerId1 = matchDTO.PlayerId1,
                PlayerId2 = matchDTO.PlayerId2
            };
            var players = await playerService.GetPlayersAsync();
            viewmodel.LoadPlayersIntoSelectList(players);

            return View(viewmodel);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await service.DeleteMatch(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
