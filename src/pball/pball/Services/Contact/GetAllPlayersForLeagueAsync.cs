namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<List<Player>>> GetAllPlayersForLeagueAsync(int LeagueID)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (db != null && db.Contacts != null && db.LeagueContacts != null)
        {
            return await Task.FromResult(Ok((from c in db.Contacts
                                             from lc in db.LeagueContacts
                                             where c.ContactID == lc.ContactID
                                             && lc.LeagueID == LeagueID
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

        return await Task.FromResult(Ok((new List<Contact>())));
    }

}


