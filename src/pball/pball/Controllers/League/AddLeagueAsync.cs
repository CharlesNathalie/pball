using System.Globalization;

namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [HttpPost]
    public async Task<ActionResult<League>> AddLeagueAsync(League league)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (LeagueService != null)
        {
            return await LeagueService.AddLeagueAsync(league);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueService")));
    }
}

