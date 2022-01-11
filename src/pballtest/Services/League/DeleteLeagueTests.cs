namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? league = (from c in db.Leagues
                                      orderby c.LeagueID
                                      select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(league);
                if (league != null)
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

                        var actionRes = await LeagueService.DeleteLeagueAsync(league.LeagueID);
                        League? leagueRet = await DoOKTestReturnLeagueAsync(actionRes);
                        Assert.NotNull(leagueRet);
                        if (leagueRet != null)
                        {
                            Assert.True(leagueRet.LeagueID > 0);
                            Assert.True(leagueRet.Removed);

                            League? leagueToChange = (from c in db.Leagues
                                                      where c.LeagueID == league.LeagueID
                                                      select c).FirstOrDefault();

                            Assert.NotNull(leagueToChange);
                            if (leagueToChange != null)
                            {
                                leagueToChange.Removed = false;
                                try
                                {
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
    public async Task DeleteLeagueAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? league = (from c in db.Leagues
                                  orderby c.LeagueID
                                  select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(league);
                if (league != null)
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

                        var actionRes = await LeagueService.AddLeagueAsync(new League());
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
    public async Task DeleteLeagueAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? league = (from c in db.Leagues
                                  orderby c.LeagueID
                                  select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(league);
                if (league != null)
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

                        league.LeagueID = 0;

                        var actionRes = await LeagueService.DeleteLeagueAsync(league.LeagueID);
                        bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                        league.LeagueID = 10000;

                        actionRes = await LeagueService.DeleteLeagueAsync(league.LeagueID);
                        boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", league.LeagueID.ToString()), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}


