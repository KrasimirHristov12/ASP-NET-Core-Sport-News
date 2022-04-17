
namespace SportNewsApp.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Enums.Fixtures;
    using SportNewsApp.Services.Fixtures;
    using System;
    using System.Threading.Tasks;

    public class FixturesViewComponent : ViewComponent
    {
        private readonly IFixturesService fixturesService;

        public FixturesViewComponent(IFixturesService fixturesService)
        {
            this.fixturesService = fixturesService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int leagueId, int teamId, DateTime from, DateTime to)
        {

            var fixtures = await this.fixturesService.GetTeamByDate((League)leagueId,teamId,from,to);
            return View(fixtures);
        }
    }
}
