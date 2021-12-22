namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [Route("{LeagueID:int}")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteLeagueAsync(int LeagueID)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (LeagueService != null)
        {
            return await LeagueService.DeleteLeagueAsync(LeagueID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueService")));
    }
}

