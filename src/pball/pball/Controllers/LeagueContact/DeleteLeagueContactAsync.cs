namespace PBall.Controllers;

public partial class LeagueContactController : ControllerBase, ILeagueContactController
{
    [Route("{LeagueContactID:int}")]
    [HttpDelete]
    public async Task<ActionResult<LeagueContact>> DeleteLeagueContactAsync(int LeagueContactID)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (LeagueContactService != null)
        {
            return await LeagueContactService.DeleteLeagueContactAsync(LeagueContactID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueContactService")));
    }
}

