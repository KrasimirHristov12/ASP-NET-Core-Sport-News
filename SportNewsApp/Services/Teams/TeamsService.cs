namespace SportNewsApp.Services.Teams
{
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using SportNewsApp.Models.Teams;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class TeamsService : ITeamsService
    {
        private readonly IConfiguration configuration;

        public TeamsService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<TeamsViewModel> GetTeamInformation(int leagueId,int teamId)
        {
            string url = $"https://v3.football.api-sports.io/teams?&league={leagueId}&id={teamId}&season=2021";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", this.configuration["ApiKeys:Football"]);
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "v3.football.api-sports.io");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await httpClient.GetAsync(url);
            string json = await result.Content.ReadAsStringAsync();
            var teamInfo = JsonConvert.DeserializeObject<TeamsViewModel>(json);
            return teamInfo;
        }
    }
}
