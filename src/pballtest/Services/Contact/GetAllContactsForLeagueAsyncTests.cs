namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllContactsForLeagueAsync_Good_Test(string culture)
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
                                            where c.ContactID == leagueContact.ContactID
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

                                    var actionRes = await ContactService.GetAllContactsForLeagueAsync(leagueContact.LeagueID);
                                    List<Contact>? contactList = await DoOKTestReturnContactListAsync(actionRes);
                                    Assert.NotNull(contactList);
                                    Assert.NotEmpty(contactList);
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
    public async Task GetAllContactsForLeagueAsync_Authorization_Error_Test(string culture)
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
                                            where c.ContactID == leagueContact.ContactID
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

                                    var actionRes = await ContactService.GetAllContactsForLeagueAsync(leagueContact.LeagueID);
                                    bool? boolRet = await DoBadRequestContactListTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
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
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllContactsForLeagueAsync_Return_Empty_List_Good_Test(string culture)
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
                    leagueContact.LeagueID = -1;

                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            where c.ContactID == leagueContact.ContactID
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

                                    var actionRes = await ContactService.GetAllContactsForLeagueAsync(leagueContact.LeagueID);
                                    List<Contact>? contactList = await DoOKTestReturnContactListAsync(actionRes);
                                    Assert.NotNull(contactList);
                                    Assert.Empty(contactList);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

