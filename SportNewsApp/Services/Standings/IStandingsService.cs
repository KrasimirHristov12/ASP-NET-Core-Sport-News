namespace SportNewsApp.Services.Standings
{
    using SportNewsApp.Models.Standings;
    using System.Threading.Tasks;
    public interface IStandingsService
    {
        Task<StandingsViewModel> GetStanding(int leagueId, int season);
    }
}
