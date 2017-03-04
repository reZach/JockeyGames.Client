using JockeyGames.Models.PingPong;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JockeyGames.Client.Services
{
    public class MatchService
    {
        private string Get { get { return "/api/matches"; } }
        private string GetId { get { return "/api/matches/{id}"; } }
        private string Post { get { return "/api/matches"; } }
        private string Put { get { return "/api/matches/{id}"; } }
        private string Delete { get { return "/api/matches/{id}"; } }

        public async Task<List<Match>> GetMatchesAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {

                return JsonConvert.DeserializeObject<List<Match>>(
                    await httpClient.GetStringAsync(URIService.Build(Get))
                );
            }
        }

        public async Task<Match> GetMatch(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<Match>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }

        public async Task<Match> AddMatch(Match match)
        {
            StringContent json = new StringContent(JsonConvert.SerializeObject(match),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage message = await httpClient.PostAsync(URIService.Build(Post), json);

                return JsonConvert.DeserializeObject<Match>(
                    await message.Content.ReadAsStringAsync()
                );
            }
        }

        public async Task<HttpResponseMessage> UpdateMatch(Match match)
        {
            string URI = Put.Replace("{id}", Convert.ToString(match.Id));
            StringContent json = new StringContent(JsonConvert.SerializeObject(match),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.PutAsync(URIService.Build(URI), json);
            }
        }

        public async Task<HttpResponseMessage> DeleteMatch(int id)
        {
            string URI = Delete.Replace("{id}", Convert.ToString(id));

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.DeleteAsync(URIService.Build(URI));
            }
        }
    }
}