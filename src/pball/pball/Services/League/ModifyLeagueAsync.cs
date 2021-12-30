namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<League>> ModifyLeagueAsync(League league)
    {
        ErrRes errRes = new ErrRes();

        if (LoggedInService.LoggedInContactInfo == null || LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (league.LeagueID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        League? leagueToModify = (from c in db.Leagues
                                  where c.LeagueID == league.LeagueID
                                  select c).FirstOrDefault();

        if (leagueToModify == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", league.LeagueID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(league.LeagueName))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        League? leagueNameExist = (from c in db.Leagues
                                  where c.LeagueID != league.LeagueID
                                  && c.LeagueName == league.LeagueName
                                  select c).AsNoTracking().FirstOrDefault();

        if (leagueNameExist != null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, "LeagueName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // almost everything is possible for the values below
        // PointsToWinners, PointsToLoosers, PlayerLevelFactor, PercentPointsFactor will all default to 0.0D

        if (leagueToModify != null)
        {
            leagueToModify.LeagueName = league.LeagueName;
            leagueToModify.PercentPointsFactor = league.PercentPointsFactor;
            leagueToModify.PlayerLevelFactor = league.PlayerLevelFactor;
            leagueToModify.PointsToLoosers = league.PointsToLoosers;
            leagueToModify.PointsToWinners = league.PointsToWinners;
            leagueToModify.LastUpdateDate_UTC = DateTime.UtcNow;
            leagueToModify.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;
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

        return await Task.FromResult(Ok(leagueToModify));
    }
}

