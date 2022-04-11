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

    public class FixturesService : IFixturesService
    {
        public async Task<FixturesViewModel> GetAll(League league, int round)
        {
            string url = $"https://v3.football.api-sports.io/fixtures?league={(int)league}&season=2021&round=Regular Season - {round}";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", "f56f03d92f3514a249a86652a12b9236");
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

        public async Task<int> GetCurrentRound(League league)
        {
            string url = $"https://v3.football.api-sports.io/fixtures/rounds?league={(int)league}&season=2021&current=true";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", "f56f03d92f3514a249a86652a12b9236");
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
