namespace PBall.Controllers;

public partial class LeagueContactController : ControllerBase, ILeagueContactController
{
    [Route("GetLeagueContacts/{LeagueID:int}")]
    [HttpGet]
    public async Task<ActionResult<List<LeagueContact>>> GetLeagueContactsAsync(int LeagueID)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            //if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (LeagueContactService != null)
        {
            return await LeagueContactService.GetLeagueContactsAsync(LeagueID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueContactService")));
    }
}

