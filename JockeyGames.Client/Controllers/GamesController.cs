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
    public class GamesController : Controller
    {
        private GamesService service = new GamesService();

        // GET: Games
        public async Task<ActionResult> Index()
        {
            return View(await service.GetGamesAsync());
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Game game = await service.GetGame(id);
                if (game == null)
                {
                    return HttpNotFound();
                }

                return View(game);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id")] Game game)
        {
            if (ModelState.IsValid)
            {
                await service.AddGame(game);
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Game game = await service.GetGame(id);
                if (game == null)
                {
                    return HttpNotFound();
                }

                return View(game);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] Game game)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateGame(game);
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Game game = await service.GetGame(id);
                if (game == null)
                {
                    return HttpNotFound();
                }

                return View(game);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await service.DeleteGame(id);       
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
