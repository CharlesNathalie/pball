namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    public async Task<ActionResult<LeagueContact>> AddLeagueContactAsync(LeagueContact leagueContact)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (leagueContact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ShouldNotBeNullOrEmpty, "leagueContact")));
        }

        if (leagueContact.LeagueID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueID")));
        }

        League? leagueExist = (from c in db.Leagues
                               where c.LeagueID == leagueContact.LeagueID
                               select c).FirstOrDefault();

        if (leagueExist != null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", leagueContact.LeagueID.ToString())));
        }

        if (leagueContact.ContactID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactID")));
        }

        Contact? contactExist = (from c in db.Contacts
                                 where c.ContactID == leagueContact.ContactID
                                 select c).FirstOrDefault();

        if (contactExist != null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContact.ContactID.ToString())));
        }


        LeagueContact leagueContactNew = new LeagueContact()
        {
            LeagueID = leagueContact.LeagueID,
            ContactID = leagueContact.ContactID,
            Removed = false,
            LastUpdateDate_UTC = DateTime.UtcNow,
            LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID,
        };

        db.LeagueContacts?.Add(leagueContactNew);
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(Ok(leagueContactNew));
    }
}

