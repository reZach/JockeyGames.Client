using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using JockeyGames.Client.Services;
using JockeyGames.Models.DTOs;

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
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PlayerDTO playerDTO = await service.GetPlayer(id.Value);
            if (playerDTO == null)
            {
                return HttpNotFound();
            }
            return View(playerDTO);
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
        public async Task<ActionResult> Create(PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                await service.AddPlayer(playerDTO);
                return RedirectToAction("Index");
            }

            return View(playerDTO);
        }

        // GET: Players/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PlayerDTO playerDTO = await service.GetPlayer(id.Value);
            if (playerDTO == null)
            {
                return HttpNotFound();
            }
            return View(playerDTO);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePlayer(playerDTO);
                return RedirectToAction("Index");
            }
            return View(playerDTO);
        }

        // GET: Players/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PlayerDTO playerDTO = await service.GetPlayer(id.Value);
            if (playerDTO == null)
            {
                return HttpNotFound();
            }
            return View(playerDTO);
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
