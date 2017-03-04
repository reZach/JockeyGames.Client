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
    public class TournamentService
    {
        private string Get { get { return "/api/tournaments"; } }
        private string GetId { get { return "/api/tournaments/{id}"; } }
        private string Post { get { return "/api/tournaments"; } }
        private string Put { get { return "/api/tournaments/{id}"; } }
        private string Delete { get { return "/api/tournaments/{id}"; } }

        public async Task<List<Tournament>> GetTournamentsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {

                return JsonConvert.DeserializeObject<List<Tournament>>(
                    await httpClient.GetStringAsync(URIService.Build(Get))
                );
            }
        }

        public async Task<Tournament> GetTournament(int id)
        {
            string URI = GetId.Replace("{id}", Convert.ToString(id));
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<Tournament>(
                    await httpClient.GetStringAsync(URIService.Build(URI))
                );
            }
        }

        public async Task<Tournament> AddTournament(Tournament tournament)
        {
            StringContent json = new StringContent(JsonConvert.SerializeObject(tournament),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage message = await httpClient.PostAsync(URIService.Build(Post), json);

                return JsonConvert.DeserializeObject<Tournament>(
                    await message.Content.ReadAsStringAsync()
                );
            }
        }

        public async Task<HttpResponseMessage> UpdateTournament(Tournament tournament)
        {
            string URI = Put.Replace("{id}", Convert.ToString(tournament.Id));
            StringContent json = new StringContent(JsonConvert.SerializeObject(tournament),
                Encoding.UTF8, "text/json");

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.PutAsync(URIService.Build(URI), json);
            }
        }

        public async Task<HttpResponseMessage> DeleteTournament(int id)
        {
            string URI = Delete.Replace("{id}", Convert.ToString(id));

            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.DeleteAsync(URIService.Build(URI));
            }
        }
    }
}