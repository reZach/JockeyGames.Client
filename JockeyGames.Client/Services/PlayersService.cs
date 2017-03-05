using JockeyGames.Models.DTOs;
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

        public async Task<List<PlayerDTO>> GetPlayersAsync()
        {
            using (HttpClient httpClient = new HttpClient()) {
                return JsonConvert.DeserializeObject<List<PlayerDTO>>(
                    await httpClient.GetStringAsync(URIService.Build(Get))
                );
            }
        }

        public async Task<PlayerDTO> GetPlayer(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<PlayerDTO>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }

        public async Task<PlayerDTO> AddPlayer(PlayerDTO player)
        {
            StringContent json = new StringContent(JsonConvert.SerializeObject(player),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage message = await httpClient.PostAsync(URIService.Build(Post), json);

                return JsonConvert.DeserializeObject<PlayerDTO>(
                    await message.Content.ReadAsStringAsync()
                );
            }
        }

        public async Task<HttpResponseMessage> UpdatePlayer(PlayerDTO player)
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