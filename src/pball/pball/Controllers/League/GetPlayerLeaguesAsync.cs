namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    [Route("GetPlayerLeagues")]
    [HttpGet]
    public async Task<ActionResult<List<League>>> GetPlayerLeaguesAsync()
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
            return await LeagueService.GetPlayerLeaguesAsync();
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "LeagueService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

