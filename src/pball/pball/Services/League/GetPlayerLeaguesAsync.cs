namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<List<League>>> GetPlayerLeaguesAsync()
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (UserService.User != null && db.LeagueContacts != null)
        {
            return await Task.FromResult(Ok((from c in db.Leagues
                                             from lc in db.LeagueContacts
                                             where c.LeagueID == lc.LeagueID
                                             && lc.ContactID == UserService.User.ContactID
                                             select c).AsNoTracking().ToList()));
        }

        return await Task.FromResult(Ok(new List<League>()));
    }
}

