namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetUserLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                orderby c.LeagueContactID
                                                select c).FirstOrDefault();

                Assert.NotNull(leagueContact);
                if (leagueContact != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            where c.ContactID == leagueContact.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull (contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;

                                if (LeagueService != null)
                                {
                                    var actionRes = await LeagueService.GetUserLeaguesAsync();
                                    List<League>? leagueListRet = await DoOKTestReturnLeagueListAsync(actionRes);
                                    Assert.NotNull(leagueListRet);
                                    Assert.NotEmpty(leagueListRet);
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
    public async Task GetUserLeagueAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                orderby c.LeagueContactID
                                                select c).FirstOrDefault();

                Assert.NotNull(leagueContact);
                if (leagueContact != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            where c.ContactID == leagueContact.ContactID
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
                                    var actionRes = await LeagueService.GetUserLeaguesAsync();
                                    bool? boolRet = await DoBadRequestLeagueListTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
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

