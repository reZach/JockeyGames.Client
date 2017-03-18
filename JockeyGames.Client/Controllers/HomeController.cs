using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JockeyGames.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult Tournament()
        {
            return View();
        }
    }
}