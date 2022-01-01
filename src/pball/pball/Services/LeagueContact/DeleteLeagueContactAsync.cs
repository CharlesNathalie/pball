namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    public async Task<ActionResult<LeagueContact>> DeleteLeagueContactAsync(int LeagueContactID)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (LeagueContactID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueContactID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        LeagueContact? leagueContact = (from c in db.LeagueContacts
                                        where c.LeagueContactID == LeagueContactID
                                        select c).FirstOrDefault(); ;

        if (leagueContact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "LeagueContactID", LeagueContactID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        leagueContact.Removed = true;
        leagueContact.LastUpdateDate_UTC = DateTime.UtcNow;
        leagueContact.LastUpdateContactID = 0;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(leagueContact));
    }
}

