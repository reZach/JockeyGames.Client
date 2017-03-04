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
    public class GamesService
    {
        private string Get { get { return "/api/games"; } }
        private string GetId { get { return "/api/games/{id}"; } }
        private string Post { get { return "/api/games"; } }
        private string Put { get { return "/api/games/{id}"; } }
        private string Delete { get { return "/api/games/{id}"; } }

        public async Task<List<Game>> GetGamesAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {

                return JsonConvert.DeserializeObject<List<Game>>(
                    await httpClient.GetStringAsync(URIService.Build(Get))
                );
            }
        }

        public async Task<Game> GetGame(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<Game>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }

        public async Task<Game> AddGame(Game game)
        {
            StringContent json = new StringContent(JsonConvert.SerializeObject(game),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage message = await httpClient.PostAsync(URIService.Build(Post), json);
                
                return JsonConvert.DeserializeObject<Game>(
                    await message.Content.ReadAsStringAsync()
                );
            }
        }

        public async Task<HttpResponseMessage> UpdateGame(Game game)
        {
            string URI = Put.Replace("{id}", Convert.ToString(game.Id));
            StringContent json = new StringContent(JsonConvert.SerializeObject(game),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.PutAsync(URIService.Build(URI), json);
            }
        }

        public async Task<HttpResponseMessage> DeleteGame(int id)
        {
            string URI = Delete.Replace("{id}", Convert.ToString(id));

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.DeleteAsync(URIService.Build(URI));
            }
        }
    }
}