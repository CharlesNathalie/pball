namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<League>> DeleteLeagueAsync(int LeagueID)
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

        League? league = (from c in db.Leagues
                      where c.LeagueID == LeagueID
                      select c).FirstOrDefault(); ;

        if (league == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", LeagueID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        league.Removed = true;
        league.LastUpdateDate_UTC = DateTime.UtcNow;
        league.LastUpdateContactID = 0;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(league));
    }
}

