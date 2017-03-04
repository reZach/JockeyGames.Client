using JockeyGames.Models.PingPong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JockeyGames.Client.Controllers
{
    public class StatsController : Controller
    {
        // GET: Stats
        public ActionResult Index()
        {
            return View();
        }        
    }
}