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
using JockeyGames.Models.PingPong;
using JockeyGames.Client.Services;

namespace JockeyGames.Client.Controllers
{
    public class TournamentsController : Controller
    {
        private TournamentService service = new TournamentService();

        // GET: Tournaments
        public async Task<ActionResult> Index()
        {
            return View(await service.GetTournamentsAsync());
        }

        // GET: Tournaments/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Tournament tournament = await service.GetTournament(id);
                if (tournament == null)
                {
                    return HttpNotFound();
                }

                return View(tournament);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // GET: Tournaments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                await service.AddTournament(tournament);
                return RedirectToAction("Index");
            }

            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Tournament tournament = await service.GetTournament(id);
                if (tournament == null)
                {
                    return HttpNotFound();
                }

                return View(tournament);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateTournament(tournament);
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Tournament tournament = await service.GetTournament(id);
                if (tournament == null)
                {
                    return HttpNotFound();
                }

                return View(tournament);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await service.DeleteTournament(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
