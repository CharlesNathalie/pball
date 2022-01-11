namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLeagueContactsAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (db != null)
        {
            LeagueContact? leagueContact = (from c in db.LeagueContacts
                                            select c).AsNoTracking().FirstOrDefault();
            Assert.NotNull(leagueContact);

            if (leagueContact != null)
            {
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

