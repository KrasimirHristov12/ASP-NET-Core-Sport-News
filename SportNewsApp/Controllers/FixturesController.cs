namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Services.Fixtures;
    using Enums.Fixtures;

    public class FixturesController : Controller
    {
        private readonly IFixturesService fixturesService;

        public FixturesController(IFixturesService fixturesService)
        {
            this.fixturesService = fixturesService;
        }
        public IActionResult Index(int? league)
        {
            var fixture = this.fixturesService.GetAll(League.PremierLeague, 30).GetAwaiter().GetResult();
            return View(fixture);
        }
    }
}
