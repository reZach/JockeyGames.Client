using JockeyGames.Models.DTOs;
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
    public class MatchesService
    {
        private string Get { get { return "/api/matches"; } }
        private string GetId { get { return "/api/matches/{id}"; } }
        private string Post { get { return "/api/matches"; } }
        private string Put { get { return "/api/matches/{id}"; } }
        private string Delete { get { return "/api/matches/{id}"; } }

        public async Task<List<MatchDTO>> GetMatchesAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<List<MatchDTO>>(
                    await httpClient.GetStringAsync(URIService.Build(Get))
                );
            }
        }

        public async Task<MatchDTO> GetMatch(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<MatchDTO>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }

        public async Task<MatchDTO> AddMatch(MatchDTO matchDTO)
        {
            StringContent json = new StringContent(JsonConvert.SerializeObject(matchDTO),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage message = await httpClient.PostAsync(URIService.Build(Post), json);

                return JsonConvert.DeserializeObject<MatchDTO>(
                    await message.Content.ReadAsStringAsync()
                );
            }
        }

        public async Task<HttpResponseMessage> UpdateMatch(MatchDTO matchDTO)
        {
            string URI = Put.Replace("{id}", Convert.ToString(matchDTO.Id));
            StringContent json = new StringContent(JsonConvert.SerializeObject(matchDTO),
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