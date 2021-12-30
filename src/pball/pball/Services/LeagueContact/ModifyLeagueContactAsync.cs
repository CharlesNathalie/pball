namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    public async Task<ActionResult<LeagueContact>> ModifyLeagueContactAsync(LeagueContact leagueContact)
    {
        ErrRes errRes = new ErrRes();

        if (LoggedInService.LoggedInContactInfo == null || LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueContact.LeagueContactID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueContactID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        LeagueContact? leagueContactToModify = (from c in db.LeagueContacts
                                                where c.LeagueID == leagueContact.LeagueID
                                                select c).FirstOrDefault();

        if (leagueContactToModify == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "LeagueContactID", leagueContact.LeagueContactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }


        if (leagueContactToModify != null)
        {
            leagueContactToModify.IsLeagueAdmin = leagueContact.IsLeagueAdmin;
            leagueContactToModify.LastUpdateDate_UTC = DateTime.UtcNow;
            leagueContactToModify.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;
        }

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(leagueContactToModify));
    }
}

