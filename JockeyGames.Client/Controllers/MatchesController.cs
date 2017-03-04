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
    public class MatchesController : Controller
    {
        private MatchService service = new MatchService();

        // GET: Matches
        public async Task<ActionResult> Index()
        {
            return View(await service.GetMatchesAsync());
        }

        // GET: Matches/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Match match = await service.GetMatch(id);
                if (match == null)
                {
                    return HttpNotFound();
                }

                return View(match);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // GET: Matches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateTime")] Match match)
        {
            if (ModelState.IsValid)
            {
                await service.AddMatch(match);
                return RedirectToAction("Index");
            }

            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Match match = await service.GetMatch(id);
                if (match == null)
                {
                    return HttpNotFound();
                }

                return View(match);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateTime")] Match match)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateMatch(match);
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Match match = await service.GetMatch(id);
                if (match == null)
                {
                    return HttpNotFound();
                }

                return View(match);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
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
