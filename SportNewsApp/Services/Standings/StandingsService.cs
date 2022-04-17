using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SportNewsApp.Models.Standings;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SportNewsApp.Services.Standings
{
    public class StandingsService : IStandingsService
    {
        private readonly IConfiguration configuration;
        public StandingsService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<StandingsViewModel> GetStanding(int leagueId, int season)
        {
            string url = $"https://v3.football.api-sports.io/standings?league={leagueId}&season={season}";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", this.configuration["ApiKeys:Football"]);
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "v3.football.api-sports.io");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await httpClient.GetAsync(url);

            string json = await result.Content.ReadAsStringAsync();
            var standings = JsonConvert.DeserializeObject<StandingsViewModel>(json);
            return standings;
        }
    }
}
