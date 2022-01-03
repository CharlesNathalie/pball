namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [Route("{LeagueID:int}")]
    [HttpDelete]
    public async Task<ActionResult<League>> DeleteLeagueAsync(int LeagueID)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (LeagueService != null)
        {
            return await LeagueService.DeleteLeagueAsync(LeagueID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueService")));
    }
}

