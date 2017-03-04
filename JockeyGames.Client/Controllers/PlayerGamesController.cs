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
    public class PlayerGamesController : Controller
    {
        private PlayerGamesService service = new PlayerGamesService();

        // GET: PlayerGames
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPlayerGamesAsync());
        }

        // GET: PlayerGames/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                PlayerGame playerGame = await service.GetPlayerGame(id);
                if (playerGame == null)
                {
                    return HttpNotFound();
                }

                return View(playerGame);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // GET: PlayerGames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlayerGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Score")] PlayerGame playerGame)
        {
            if (ModelState.IsValid)
            {
                await service.AddPlayerGame(playerGame);
                return RedirectToAction("Index");
            }

            return View(playerGame);
        }

        // GET: PlayerGames/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                PlayerGame playerGame = await service.GetPlayerGame(id);
                if (playerGame == null)
                {
                    return HttpNotFound();
                }

                return View(playerGame);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: PlayerGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Score")] PlayerGame playerGame)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePlayerGame(playerGame);
                return RedirectToAction("Index");
            }
            return View(playerGame);
        }

        // GET: PlayerGames/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                PlayerGame playerGame = await service.GetPlayerGame(id);
                if (playerGame == null)
                {
                    return HttpNotFound();
                }

                return View(playerGame);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: PlayerGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await service.DeletePlayerGame(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
