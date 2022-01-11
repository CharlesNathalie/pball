namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_Good_Test(string culture)
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

                        League leagueToModify = new League()
                        {
                            LeagueID = leagueInDB.LeagueID,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor + 0.1D,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor + 0.1D,
                            PointsToLoosers = leagueInDB.PointsToLoosers + 0.1D,
                            PointsToWinners = leagueInDB.PointsToWinners + 0.1D,
                        };

                        var actionModifyRes = await LeagueService.ModifyLeagueAsync(leagueToModify);
                        League? leagueRet = await DoOKTestReturnLeagueAsync(actionModifyRes);
                        Assert.NotNull(leagueRet);
                        if (leagueRet != null)
                        {
                            Assert.Equal(leagueToModify.LeagueID, leagueRet.LeagueID);
                            Assert.Equal(leagueToModify.LeagueName, leagueRet.LeagueName);
                            Assert.Equal(leagueToModify.PercentPointsFactor, leagueRet.PercentPointsFactor);
                            Assert.Equal(leagueToModify.PointsToLoosers, leagueRet.PointsToLoosers);
                            Assert.Equal(leagueToModify.PointsToWinners, leagueRet.PointsToWinners);
                            Assert.Equal(leagueToModify.PlayerLevelFactor, leagueRet.PlayerLevelFactor);

                            League leagueToModify2 = new League()
                            {
                                LeagueID = leagueToModify.LeagueID,
                                LeagueName = leagueInDB.LeagueName,
                                PercentPointsFactor = leagueInDB.PercentPointsFactor,
                                PlayerLevelFactor = leagueInDB.PlayerLevelFactor,
                                PointsToLoosers = leagueInDB.PointsToLoosers,
                                PointsToWinners = leagueInDB.PointsToWinners,
                            };

                            var actionModifyRes2 = await LeagueService.ModifyLeagueAsync(leagueToModify2);
                            League? leagueRet2 = await DoOKTestReturnLeagueAsync(actionModifyRes2);
                            Assert.NotNull(leagueRet2);
                            if (leagueRet2 != null)
                            {
                                Assert.Equal(leagueToModify2.LeagueID, leagueRet2.LeagueID);
                                Assert.Equal(leagueToModify2.LeagueName, leagueRet2.LeagueName);
                                Assert.Equal(leagueToModify2.PercentPointsFactor, leagueRet2.PercentPointsFactor);
                                Assert.Equal(leagueToModify2.PointsToLoosers, leagueRet2.PointsToLoosers);
                                Assert.Equal(leagueToModify2.PointsToWinners, leagueRet2.PointsToWinners);
                                Assert.Equal(leagueToModify2.PlayerLevelFactor, leagueRet2.PlayerLevelFactor);
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
    public async Task ModifyLeagueAsync_Authorization_Error_Test(string culture)
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

                        League leagueToModify = new League()
                        {
                            LeagueID = leagueInDB.LeagueID,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor + 0.1D,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor + 0.1D,
                            PointsToLoosers = leagueInDB.PointsToLoosers + 0.1D,
                            PointsToWinners = leagueInDB.PointsToWinners + 0.1D,
                        };

                        var actionModifyRes = await LeagueService.ModifyLeagueAsync(new League());
                        bool? boolRet = await DoBadRequestLeagueTestAsync(PBallRes.YouDoNotHaveAuthorization, actionModifyRes);
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
    public async Task ModifyLeagueAsync_LeagueID_Error_Test(string culture)
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
                            LeagueID = leagueInDB.LeagueID,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor + 0.1D,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor + 0.1D,
                            PointsToLoosers = leagueInDB.PointsToLoosers + 0.1D,
                            PointsToWinners = leagueInDB.PointsToWinners + 0.1D,
                        };

                        league.LeagueID = 0;

                        var actionModifyRes = await LeagueService.ModifyLeagueAsync(league);
                        bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionModifyRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                        league.LeagueID = 10000;

                        actionModifyRes = await LeagueService.ModifyLeagueAsync(league);
                        boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", league.LeagueID.ToString()), actionModifyRes);
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
    public async Task ModifyLeagueAsync_LeagueName_Required_Error_Test(string culture)
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

                        League leagueRet = new League()
                        {
                            LeagueID = leagueInDB.LeagueID,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor + 0.1D,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor + 0.1D,
                            PointsToLoosers = leagueInDB.PointsToLoosers + 0.1D,
                            PointsToWinners = leagueInDB.PointsToWinners + 0.1D,
                        };

                        leagueRet.LeagueName = "";

                        var actionModifyRes = await LeagueService.ModifyLeagueAsync(leagueRet);
                        bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueName"), actionModifyRes);
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
    public async Task ModifyLeagueAsync_LeagueName_AlreadyTaken_Error_Test(string culture)
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

                        League leagueRet = new League()
                        {
                            LeagueID = leagueInDB.LeagueID,
                            LeagueName = leagueInDB.LeagueName + " more",
                            PercentPointsFactor = leagueInDB.PercentPointsFactor + 0.1D,
                            PlayerLevelFactor = leagueInDB.PlayerLevelFactor + 0.1D,
                            PointsToLoosers = leagueInDB.PointsToLoosers + 0.1D,
                            PointsToWinners = leagueInDB.PointsToWinners + 0.1D,
                        };

                        leagueRet.LeagueName = "";

                        var actionModifyRes = await LeagueService.ModifyLeagueAsync(leagueRet);
                        bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueName"), actionModifyRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}

