namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<bool>> DeleteLeagueAsync(int LeagueID)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (LeagueID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueID")));
        }

        League? league = (from c in db.Leagues
                      where c.LeagueID == LeagueID
                      select c).FirstOrDefault(); ;

        if (league == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", LeagueID.ToString())));
        }

        league.Removed = true;
        league.LastUpdateDate_UTC = DateTime.UtcNow;
        league.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;

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

