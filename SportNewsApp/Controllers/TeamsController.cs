using Microsoft.AspNetCore.Mvc;
using SportNewsApp.Services.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportNewsApp.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }
        public async Task<IActionResult> Information(int leagueId, int teamId)
        {
            var team = await this.teamsService.GetTeamInformation(leagueId,teamId);
            ViewData["leagueId"] = leagueId;
            ViewData["teamId"] = teamId;

            return View(team);
        }
    }
}
