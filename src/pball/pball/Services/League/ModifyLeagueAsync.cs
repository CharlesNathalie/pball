namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<League>> ModifyLeagueAsync(League league)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (league == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ShouldNotBeNullOrEmpty, "league")));
        }

        if (string.IsNullOrWhiteSpace(league.LeagueName))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GroupName")));
        }

        // almost everything is possible for the values below
        // PointsToWinners, PointsToLoosers, PlayerLevelFactor, PercentPointsFactor will all default to 0.0D

        League? groupExist = (from c in db.Leagues
                            where c.LeagueName == league.LeagueName
                            select c).FirstOrDefault();

        League groupNew = new League()
        {
            LeagueName = league.LeagueName,
            CreatorContactID = league.CreatorContactID,
            PercentPointsFactor = league.PercentPointsFactor,
            PlayerLevelFactor = league.PlayerLevelFactor,
            PointsToLoosers = league.PointsToLoosers,
            PointsToWinners = league.PointsToWinners,
            Removed = false,
            LastUpdateDate_UTC = DateTime.UtcNow,
            LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID,
        };

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(Ok(groupNew));
    }
}

