using Microsoft.AspNetCore.Mvc;
using SportNewsApp.Services.Teams;
using System.Threading.Tasks;

namespace SportNewsApp.ViewComponents
{
    public class SquadsViewComponent : ViewComponent
    {
        private readonly ITeamsService teamsService;

        public SquadsViewComponent(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var teamSquadInfo = await this.teamsService.GetTeamSquad(teamId);
            return View(teamSquadInfo);
        }
    }
}
