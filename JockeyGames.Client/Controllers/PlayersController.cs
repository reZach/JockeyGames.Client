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
using JockeyGames.Models.Shared;
using JockeyGames.Client.Services;

namespace JockeyGames.Client.Controllers
{
    public class PlayersController : Controller
    {
        private PlayersService service = new PlayersService();

        // GET: Players
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPlayersAsync());
        }

        // GET: Players/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Player player = await service.GetPlayer(id);
                if (player == null)
                {
                    return HttpNotFound();
                }

                return View(player);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Player player)
        {
            if (ModelState.IsValid)
            {
                await service.AddPlayer(player);
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Player player = await service.GetPlayer(id);
                if (player == null)
                {
                    return HttpNotFound();
                }

                return View(player);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Player player)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePlayer(player);
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Player player = await service.GetPlayer(id);
                if (player == null)
                {
                    return HttpNotFound();
                }

                return View(player);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {            
            await service.DeletePlayer(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
