namespace SportNewsApp.Services.Fixtures
{
    using SportNewsApp.Enums.Fixtures;
    using SportNewsApp.Models.Fixtures;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using System;

    public class FixturesService : IFixturesService
    {
        private readonly IConfiguration configuration;
        public FixturesService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<FixturesViewModel> GetAll(League league, int round)
        {
            string url = $"https://v3.football.api-sports.io/fixtures?league={(int)league}&season=2021&round=Regular Season - {round}";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", this.configuration["ApiKeys:Football"]);
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "v3.football.api-sports.io");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await httpClient.GetAsync(url);

            string json = await result.Content.ReadAsStringAsync();
            var fixtures = JsonConvert.DeserializeObject<FixturesViewModel>(json);
            string leagueName = string.Empty;
            switch (league)
            {
                case League.PremierLeague:
                    leagueName = "Premier League";
                    break;
                case League.Parvaliga:
                    leagueName = "A grupa";
                    break;
                case League.LigueOne:
                    leagueName = "Ligue 1";
                    break;
                default:
                    break;
            }
            fixtures.LeagueName = leagueName;
            return fixtures;

        }

        public async Task<FixturesViewModel> GetTeamByDate(League league, int teamId, DateTime from, DateTime to)
        {
            string fromFormatted = from.ToString("yyyy-MM-dd");
            string toFormatted = to.ToString("yyyy-MM-dd");
            int leagueId = (int)league;

            string url = $"https://v3.football.api-sports.io/fixtures?league={leagueId}&season=2021&team={teamId}&from={fromFormatted}&to={toFormatted}";
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", this.configuration["ApiKeys:Football"]);
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "v3.football.api-sports.io");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await httpClient.GetAsync(url);

            string json = await result.Content.ReadAsStringAsync();
            var fixtures = JsonConvert.DeserializeObject<FixturesViewModel>(json);
            return fixtures;

        }

        public async Task<int> GetCurrentRound(League league)
        {
            string url = $"https://v3.football.api-sports.io/fixtures/rounds?league={(int)league}&season=2021&current=true";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", this.configuration["ApiKeys:Football"]);
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "v3.football.api-sports.io");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await httpClient.GetAsync(url);

            string json = await result.Content.ReadAsStringAsync();
            var response = new { Response = new string[1]};
            var round = JsonConvert.DeserializeAnonymousType(json, response);
            return int.Parse(round.Response[0].Replace("Regular Season - ", ""));
            
        }
    }
}
