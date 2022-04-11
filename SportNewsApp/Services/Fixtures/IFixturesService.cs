namespace SportNewsApp.Services.Fixtures
{
    using SportNewsApp.Enums.Fixtures;
    using SportNewsApp.Models.Fixtures;
    using System.Threading.Tasks;
    public interface IFixturesService
    {
        Task<FixturesViewModel> GetAll(League league, int round);
        Task<int> GetCurrentRound(League league);
    }
}
