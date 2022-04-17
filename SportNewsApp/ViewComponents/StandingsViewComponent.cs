namespace SportNewsApp.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Services.Standings;
    using System.Threading.Tasks;

    public class StandingsViewComponent : ViewComponent
    {
        private readonly IStandingsService standingsService;

        public StandingsViewComponent(IStandingsService standingsService)
        {
            this.standingsService = standingsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int leagueId, int season)
        {

            var standings = await this.standingsService.GetStanding(leagueId, season);
            
            return View(standings);
        }
    }
}
