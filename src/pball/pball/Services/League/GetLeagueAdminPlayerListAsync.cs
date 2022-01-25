namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<List<Player>>> GetLeagueAdminPlayerListAsync(int LeagueID)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (LeagueID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (UserService.User != null)
        {
            if (db.LeagueContacts != null && db.Contacts != null)
            {
                return await Task.FromResult(Ok((from c in db.Contacts
                                                 from lc in db.LeagueContacts
                                                 where c.ContactID == lc.ContactID
                                                 && lc.IsLeagueAdmin == true
                                                 select new Player
                                                 {
                                                     ContactID = c.ContactID,
                                                     FirstName = c.FirstName,
                                                     Initial = c.Initial,
                                                     LastName = c.LastName,
                                                     LoginEmail = c.LoginEmail,
                                                     PlayerLevel = c.PlayerLevel,
                                                     Removed = c.Removed,
                                                 }).AsNoTracking().ToList()));
            }
        }

        return await Task.FromResult(Ok(new List<League>()));
    }
}

