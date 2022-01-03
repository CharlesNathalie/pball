namespace PBall.Controllers;

public partial class LeagueContactController : ControllerBase, ILeagueContactController
{
    [HttpPut]
    public async Task<ActionResult<LeagueContact>> ModifyLeagueContactAsync(LeagueContact leagueContact)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (LeagueContactService != null)
        {
            return await LeagueContactService.ModifyLeagueContactAsync(leagueContact);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueService")));
    }
}

