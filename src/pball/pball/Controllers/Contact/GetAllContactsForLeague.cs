namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("GetAllContactsForLeague/{LeagueID:int}")]
    [HttpPost]
    public async Task<ActionResult<List<Contact>>> GetAllContactsForLeagueAsync(int LeagueID)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
        }

        if (ContactService != null)
        {
            return await ContactService.GetAllContactsForLeagueAsync(LeagueID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

