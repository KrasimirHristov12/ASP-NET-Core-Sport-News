namespace SportNewsApp.Services.Fixtures
{
    using SportNewsApp.Enums.Fixtures;
    using SportNewsApp.Models.Fixtures;
    using System;
    using System.Threading.Tasks;
    public interface IFixturesService
    {
        Task<FixturesViewModel> GetAll(League league, int round);
        Task<FixturesViewModel> GetTeamByDate(League league, int teamId, DateTime from, DateTime to);
        Task<int> GetCurrentRound(League league);
    }
}
