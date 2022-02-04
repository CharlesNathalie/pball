namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task SearchContactsAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                select c).FirstOrDefault();

                Assert.NotNull(leagueContact);
                if (leagueContact != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            from lc in db.LeagueContacts
                                            where c.ContactID == lc.ContactID
                                            && lc.LeagueID != leagueContact.LeagueID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(ContactService);
                            if (ContactService != null)
                            {
                                Assert.NotNull(UserService);
                                if (UserService != null)
                                {
                                    UserService.User = contact;

                                    string SearchTerms = "Charles";

                                    var actionRes = await ContactService.SearchContactsAsync(leagueContact.LeagueID, SearchTerms);
                                    List<Player>? playerList = await DoOKTestReturnPlayerListAsync(actionRes);
                                    Assert.NotNull(playerList);
                                    Assert.NotEmpty(playerList);
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
    public async Task SearchContactsAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                Assert.NotNull(db.LeagueContacts);
                if (db.LeagueContacts != null)
                {
                    LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                    select c).FirstOrDefault();

                    Assert.NotNull(leagueContact);
                    if (leagueContact != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contact = (from c in db.Contacts
                                                select c).FirstOrDefault();

                            Assert.NotNull(contact);
                            if (contact != null)
                            {
                                Assert.NotNull(ContactService);
                                if (ContactService != null)
                                {
                                    Assert.NotNull(UserService);
                                    if (UserService != null)
                                    {
                                        UserService.User = null;
                                        string SearchTerms = "Charles";

                                        var actionRes = await ContactService.SearchContactsAsync(leagueContact.LeagueID, SearchTerms);
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
}

