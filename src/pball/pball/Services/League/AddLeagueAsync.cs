namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<League>> AddLeagueAsync(League league)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(league.LeagueName))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // PointsToWinners, PointsToLoosers, PlayerLevelFactor, PercentPointsFactor will all default to 0.0D

        League? leagueExist = (from c in db.Leagues
                               where c.LeagueName == league.LeagueName
                               select c).AsNoTracking().FirstOrDefault();

        if (leagueExist != null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._AlreadyTaken, "LeagueName"));
            return await Task.FromResult(BadRequest(errRes));
        }

        League leagueNew = new League()
        {
            LeagueName = league.LeagueName,
            PercentPointsFactor = league.PercentPointsFactor,
            PlayerLevelFactor = league.PlayerLevelFactor,
            PointsToLosers = league.PointsToLosers,
            PointsToWinners = league.PointsToWinners,
            Removed = false,
            LastUpdateDate_UTC = DateTime.UtcNow,
            LastUpdateContactID = UserService.User.ContactID,
        };

        if (db.Leagues != null)
        {
            db.Leagues.Add(leagueNew);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        return await Task.FromResult(Ok(leagueNew));
    }
}

