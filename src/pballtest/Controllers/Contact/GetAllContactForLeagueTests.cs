namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllContactsForLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        if (db != null && db.Contacts != null && db.LeagueContacts != null)
        {
            LeagueContact? leagueContact = (from c in db.LeagueContacts
                                            select c).FirstOrDefault();
            Assert.NotNull(leagueContact);

            if (leagueContact != null)
            {
                List<Contact>? contactList = await DoOkGetAllContactsForLeague(leagueContact.LeagueID, culture);
                Assert.NotNull(contactList);
                Assert.NotEmpty(contactList);
            }
        }
    }
}

