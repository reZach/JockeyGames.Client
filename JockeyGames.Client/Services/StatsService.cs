using JockeyGames.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace JockeyGames.Client.Services
{
    public class StatsService
    {
        private string GetId { get { return "/api/stats/{id}"; } }

        public async Task<List<MatchDTO>> GetStats(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<List<MatchDTO>>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }
    }
}