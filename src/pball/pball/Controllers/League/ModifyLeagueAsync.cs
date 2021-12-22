namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [HttpPut]
    public async Task<ActionResult<League>> ModifyLeagueAsync(League league)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (LeagueService != null)
        {
            return await LeagueService.ModifyLeagueAsync(league);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueService")));
    }
}

