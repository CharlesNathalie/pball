namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    public async Task<ActionResult<List<LeagueContact>>> GetLeagueContactsAsync(int LeagueID)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok((from c in db.LeagueContacts
                                         where c.LeagueID == LeagueID
                                         select c).AsNoTracking().ToList()));
    }
}

