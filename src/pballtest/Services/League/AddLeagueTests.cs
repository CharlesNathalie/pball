namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? leagueInDB = (from c in db.Leagues
                                      orderby c.LeagueID
                                      select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(leagueInDB);
                if (leagueInDB != null)
                {
                    Assert.NotNull(LeagueService);
                    if (LeagueService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contact = (from c in db.Contacts
                                                orderby c.ContactID
                                                select c).AsNoTracking().FirstOrDefault();

                            Assert.NotNull(contact);
                            if (contact != null)
                            {
                                Assert.NotNull(UserService);
                                if (UserService != null)
                                {
                                    UserService.User = contact;
                                }
                            }
                        }

                        League league = new League()
                        {
                            LeagueID = 0,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor,
                            PointsToLosers = leagueInDB.PointsToLosers,
                            PointsToWinners = leagueInDB.PointsToWinners,
                            Removed = false,
                            LastUpdateContactID = leagueInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueInDB.LastUpdateDate_UTC
                        };

                        var actionRes = await LeagueService.AddLeagueAsync(league);
                        League? leagueRet = await DoOKTestReturnLeagueAsync(actionRes);

                        Assert.NotNull(leagueRet);
                        if (leagueRet != null)
                        {
                            League? leagueToDelete = (from c in db.Leagues
                                                      where c.LeagueID == leagueRet.LeagueID
                                                      select c).FirstOrDefault();

                            Assert.NotNull(leagueToDelete);
                            if (leagueToDelete != null)
                            {
                                try
                                {
                                    db.Leagues.Remove(leagueToDelete);
                                    db.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    Assert.True(false, ex.Message);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? leagueInDB = (from c in db.Leagues
                                      orderby c.LeagueID
                                      select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(leagueInDB);
                if (leagueInDB != null)
                {
                    Assert.NotNull(LeagueService);
                    if (LeagueService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contact = (from c in db.Contacts
                                                orderby c.ContactID
                                                select c).AsNoTracking().FirstOrDefault();

                            Assert.NotNull(contact);
                            if (contact != null)
                            {
                                Assert.NotNull(UserService);
                                if (UserService != null)
                                {
                                    UserService.User = null;
                                }
                            }
                        }

                        League league = new League()
                        {
                            LeagueID = 0,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor,
                            PointsToLosers = leagueInDB.PointsToLosers,
                            PointsToWinners = leagueInDB.PointsToWinners,
                            Removed = false,
                            LastUpdateContactID = leagueInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueInDB.LastUpdateDate_UTC
                        };

                        var actionRes = await LeagueService.AddLeagueAsync(league);
                        bool? boolRet = await DoBadRequestLeagueTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_LeagueName_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? leagueInDB = (from c in db.Leagues
                                      orderby c.LeagueID
                                      select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(leagueInDB);
                if (leagueInDB != null)
                {
                    Assert.NotNull(LeagueService);
                    if (LeagueService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contact = (from c in db.Contacts
                                                orderby c.ContactID
                                                select c).AsNoTracking().FirstOrDefault();

                            Assert.NotNull(contact);
                            if (contact != null)
                            {
                                Assert.NotNull(UserService);
                                if (UserService != null)
                                {
                                    UserService.User = contact;
                                }
                            }
                        }

                        League league = new League()
                        {
                            LeagueID = 0,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor,
                            PointsToLosers = leagueInDB.PointsToLosers,
                            PointsToWinners = leagueInDB.PointsToWinners,
                            Removed = false,
                            LastUpdateContactID = leagueInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueInDB.LastUpdateDate_UTC
                        };

                        league.LeagueName = "";

                        var actionRes = await LeagueService.AddLeagueAsync(league);
                        bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueName"), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_LeagueName_AlreadyTaken_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? leagueInDB = (from c in db.Leagues
                                      orderby c.LeagueID
                                      select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(leagueInDB);
                if (leagueInDB != null)
                {
                    Assert.NotNull(LeagueService);
                    if (LeagueService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contact = (from c in db.Contacts
                                                orderby c.ContactID
                                                select c).AsNoTracking().FirstOrDefault();

                            Assert.NotNull(contact);
                            if (contact != null)
                            {
                                Assert.NotNull(UserService);
                                if (UserService != null)
                                {
                                    UserService.User = contact;
                                }
                            }
                        }

                        League league = new League()
                        {
                            LeagueID = 0,
                            LeagueName = leagueInDB.LeagueName,
                            PercentPointsFactor = leagueInDB.PercentPointsFactor,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor,
                            PointsToLosers = leagueInDB.PointsToLosers,
                            PointsToWinners = leagueInDB.PointsToWinners,
                            Removed = false,
                            LastUpdateContactID = leagueInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueInDB.LastUpdateDate_UTC
                        };

                        var actionRes = await LeagueService.AddLeagueAsync(league);
                        bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._AlreadyTaken, "LeagueName"), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}

