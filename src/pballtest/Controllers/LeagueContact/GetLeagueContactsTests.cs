namespace pball.Controllers.Tests;

public partial class LeagueContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLeagueContactsAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

        if (db != null)
        {
            LeagueContact? leagueContact = (from c in db.LeagueContacts
                                            select c).FirstOrDefault();
            Assert.NotNull(leagueContact);

            if (leagueContact != null)
            {
                List<LeagueContact>? leagueContactList = await DoOkGetLeagueContacts(leagueContact.LeagueID, culture);
                Assert.NotNull(leagueContactList);
                Assert.NotEmpty(leagueContactList);
            }
        }
    }
}

