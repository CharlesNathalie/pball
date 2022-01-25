namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLeagueAdminPlayerListListAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? league = (from c in db.Leagues
                                 select c).FirstOrDefault();

                Assert.NotNull(league);
                if (league != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;

                                if (LeagueService != null)
                                {
                                    var actionRes = await LeagueService.GetLeagueAdminPlayerListAsync(league.LeagueID);
                                    List<Player>? playerListRet = await DoOKTestReturnPlayerListAsync(actionRes);
                                    Assert.NotNull(playerListRet);
                                    Assert.NotEmpty(playerListRet);
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
    public async Task GetLeagueAdminPlayerListAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? league = (from c in db.Leagues
                                  select c).FirstOrDefault();

                Assert.NotNull(league);
                if (league != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = null;

                                if (LeagueService != null)
                                {
                                    var actionRes = await LeagueService.GetLeagueAdminPlayerListAsync(league.LeagueID);
                                    bool? boolRet = await DoBadRequestPlayerListTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                                    Assert.NotNull(boolRet);
                                    Assert.True(boolRet);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

