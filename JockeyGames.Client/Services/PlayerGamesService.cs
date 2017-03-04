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
    public class PlayerGamesService
    {
        private string Get { get { return "/api/playergames"; } }
        private string GetId { get { return "/api/playergames/{id}"; } }
        private string Post { get { return "/api/playergames"; } }
        private string Put { get { return "/api/playergames/{id}"; } }
        private string Delete { get { return "/api/playergames/{id}"; } }

        public async Task<List<PlayerGame>> GetPlayerGamesAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {

                return JsonConvert.DeserializeObject<List<PlayerGame>>(
                    await httpClient.GetStringAsync(URIService.Build(Get))
                );
            }
        }

        public async Task<PlayerGame> GetPlayerGame(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<PlayerGame>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }

        public async Task<PlayerGame> AddPlayerGame(PlayerGame playerGame)
        {
            StringContent json = new StringContent(JsonConvert.SerializeObject(playerGame),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage message = await httpClient.PostAsync(URIService.Build(Post), json);

                return JsonConvert.DeserializeObject<PlayerGame>(
                    await message.Content.ReadAsStringAsync()
                );
            }
        }

        public async Task<HttpResponseMessage> UpdatePlayerGame(PlayerGame playerGame)
        {
            /*string URI = Put.Replace("{id}", Convert.ToString(playerGame.Id));
            StringContent json = new StringContent(JsonConvert.SerializeObject(playerGame),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.PutAsync(URIService.Build(URI), json);
            }*/
            StringContent json = new StringContent(JsonConvert.SerializeObject(playerGame),
                Encoding.UTF8, "text/json");
            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.PutAsync(URIService.Build(Put), json);
            }
        }

        public async Task<HttpResponseMessage> DeletePlayerGame(int id)
        {
            string URI = Delete.Replace("{id}", Convert.ToString(id));

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.DeleteAsync(URIService.Build(URI));
            }
        }
    }
}