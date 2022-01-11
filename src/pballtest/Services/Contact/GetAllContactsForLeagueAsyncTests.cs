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
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.GetAllContactsForLeagueAsync(leagueContact.LeagueID);
                        List<Contact>? contactList = await DoOKTestReturnContactListAsync(actionRes);
                        Assert.NotNull(contactList);
                        Assert.NotEmpty(contactList);
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

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
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
