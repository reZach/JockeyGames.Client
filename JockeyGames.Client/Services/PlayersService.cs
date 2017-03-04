using JockeyGames.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JockeyGames.Client.Services
{
    public class PlayersService
    {
        private string Get { get { return "/api/players"; } }
        private string GetId { get { return "/api/players/{id}"; } }
        private string Post { get { return "/api/players"; } }
        private string Put { get { return "/api/players/{id}"; } }
        private string Delete { get { return "/api/players/{id}"; } }

        public async Task<List<Player>> GetPlayersAsync()
        {
            using (HttpClient httpClient = new HttpClient()) {

                return JsonConvert.DeserializeObject<List<Player>>(
                    await httpClient.GetStringAsync(URIService.Build(Get))
                );
            }
        }

        public async Task<Player> GetPlayer(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<Player>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }

        public async Task<Player> AddPlayer(Player player)
        {
            StringContent json = new StringContent(JsonConvert.SerializeObject(player),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage message = await httpClient.PostAsync(URIService.Build(Post), json);

                return JsonConvert.DeserializeObject<Player>(
                    await message.Content.ReadAsStringAsync()
                );
            }
        }

        public async Task<HttpResponseMessage> UpdatePlayer(Player player)
        {
            string URI = Put.Replace("{id}", Convert.ToString(player.Id));
            StringContent json = new StringContent(JsonConvert.SerializeObject(player),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.PutAsync(URIService.Build(URI), json);
            }
        }

        public async Task<HttpResponseMessage> DeletePlayer(int id)
        {
            string URI = Delete.Replace("{id}", Convert.ToString(id));

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.DeleteAsync(URIService.Build(URI));
            }
        }
    }
}