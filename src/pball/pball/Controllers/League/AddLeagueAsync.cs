using System.Globalization;

namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [HttpPost]
    public async Task<ActionResult<League>> AddLeagueAsync(League league)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (LeagueService != null)
        {
            return await LeagueService.AddLeagueAsync(league);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueService")));
    }
}

