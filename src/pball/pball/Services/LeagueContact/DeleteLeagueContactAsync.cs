namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    public async Task<ActionResult<bool>> DeleteLeagueContactAsync(int LeagueContactID)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (LeagueContactID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueContactID")));
        }

        LeagueContact? leagueContact = (from c in db.LeagueContacts
                                        where c.LeagueContactID == LeagueContactID
                                        select c).FirstOrDefault(); ;

        if (leagueContact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "LeagueContactID", LeagueContactID.ToString())));
        }

        leagueContact.Removed = true;
        leagueContact.LastUpdateDate_UTC = DateTime.UtcNow;
        leagueContact.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(Ok(true));
    }
}

