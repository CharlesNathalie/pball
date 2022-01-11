using System.Globalization;

namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [Route("GetAllLeagues")]
    [HttpGet]
    public async Task<ActionResult<List<League>>> GetAllLeaguesAsync()
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            //if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (LeagueService != null)
        {
            return await LeagueService.GetAllLeaguesAsync();
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueService")));
    }
}

