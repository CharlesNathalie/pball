namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    public async Task<ActionResult<LeagueContact>> AddLeagueContactAsync(LeagueContact leagueContact)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueContact.LeagueContactID != 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._ShouldBeEqualTo_, "LeagueContactID", "0"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueContact.LeagueID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        League? leagueExist = (from c in db.Leagues
                               where c.LeagueID == leagueContact.LeagueID
                               select c).FirstOrDefault();

        if (leagueExist == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", leagueContact.LeagueID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueContact.ContactID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "ContactID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contactExist = (from c in db.Contacts
                                 where c.ContactID == leagueContact.ContactID
                                 select c).FirstOrDefault();

        if (contactExist == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContact.ContactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        LeagueContact? leagueContactExist = (from c in db.LeagueContacts
                                             where c.LeagueID == leagueContact.LeagueID
                                             && c.ContactID == leagueContact.ContactID
                                             select c).FirstOrDefault();

        if (leagueContactExist != null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._AlreadyExist, "LeagueContact"));
            return await Task.FromResult(BadRequest(errRes));
        }

        LeagueContact leagueContactNew = new LeagueContact()
        {
            LeagueID = leagueContact.LeagueID,
            ContactID = leagueContact.ContactID,
            IsLeagueAdmin = leagueContact.IsLeagueAdmin,
            Removed = false,
            LastUpdateDate_UTC = DateTime.UtcNow,
            LastUpdateContactID = 0,
        };

        db.LeagueContacts?.Add(leagueContactNew);
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(leagueContactNew));
    }
}

