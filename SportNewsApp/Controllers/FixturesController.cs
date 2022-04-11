namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Services.Fixtures;
    using Enums.Fixtures;
    using System;

    public class FixturesController : Controller
    {
        private readonly IFixturesService fixturesService;

        public FixturesController(IFixturesService fixturesService)
        {
            this.fixturesService = fixturesService;
        }
        public IActionResult Index(int league)
        {
            var getCurrentRound = this.fixturesService.GetCurrentRound((League)league).GetAwaiter().GetResult();


            var leagueEnum = (League)league;
            var fixture = this.fixturesService.GetAll(leagueEnum, getCurrentRound).GetAwaiter().GetResult();

            ViewData["round"] = getCurrentRound;



            return View(fixture);
        }
    }
}
