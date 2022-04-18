namespace SportNewsApp.Services.Teams
{
    using SportNewsApp.Models.Squads;
    using SportNewsApp.Models.Teams;
    using System.Threading.Tasks;
    public interface ITeamsService
    {
        Task<TeamsViewModel> GetTeamInformation(int leagueId, int teamId);
        Task<SquadsViewModel> GetTeamSquad(int teamId);

    }
}
