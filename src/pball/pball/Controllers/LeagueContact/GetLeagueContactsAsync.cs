namespace PBall.Controllers;

public partial class LeagueContactController : ControllerBase, ILeagueContactController
{
    [Route("GetLeagueContacts/{LeagueID:int}")]
    [HttpGet]
    public async Task<ActionResult<List<LeagueContact>>> GetLeagueContactsAsync(int LeagueID)
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

        if (LeagueContactService != null)
        {
            return await LeagueContactService.GetLeagueContactsAsync(LeagueID);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "LeagueContactService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

