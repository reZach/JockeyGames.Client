using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JockeyGames.Client.Services
{
    public static class URIService
    {
        public static string Build(string s)
        {
            if (ConfigurationManager.AppSettings["UseLocalAPIBase"] == "true")
            {
                return "http://localhost:53518" + s;
            }
            return ConfigurationManager.AppSettings["JockeyGamesAPIBase"].ToString() + s;
        }
    }
}