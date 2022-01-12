namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [Route("{LeagueID:int}")]
    [HttpDelete]
    public async Task<ActionResult<League>> DeleteLeagueAsync(int LeagueID)
    {
        ErrRes errRes = new ErrRes();

        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData))
            {
                errRes.ErrList.Add(string.Format(PBallRes.LanguageNotSelected));
                return await Task.FromResult(BadRequest(errRes));
            }
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request))
            {
                errRes.ErrList.Add(string.Format(PBallRes.YouDoNotHaveAuthorization));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        if (LeagueService != null)
        {
            return await LeagueService.DeleteLeagueAsync(LeagueID);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "LeagueService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

