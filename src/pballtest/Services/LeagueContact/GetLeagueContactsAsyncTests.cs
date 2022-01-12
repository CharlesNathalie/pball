namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLeagueContactsAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            LeagueContact? leagueContact = (from c in db.LeagueContacts
                                            select c).AsNoTracking().FirstOrDefault();
            Assert.NotNull(leagueContact);

            if (leagueContact != null)
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

                            if (LeagueContactService != null)
                            {
                                var actionRes = await LeagueContactService.GetLeagueContactsAsync(leagueContact.LeagueID);
                                List<LeagueContact>? leagueContactListRet = await DoOKTestReturnLeagueContactListAsync(actionRes);
                                Assert.NotNull(leagueContactListRet);
                                Assert.NotEmpty(leagueContactListRet);
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
    public async Task GetLeagueContactsAsync_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            LeagueContact? leagueContact = (from c in db.LeagueContacts
                                            select c).AsNoTracking().FirstOrDefault();
            Assert.NotNull(leagueContact);

            if (leagueContact != null)
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

                            if (LeagueContactService != null)
                            {
                                var actionRes = await LeagueContactService.GetLeagueContactsAsync(leagueContact.LeagueID);
                                bool? boolRet = await DoBadRequestLeagueContactListTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
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