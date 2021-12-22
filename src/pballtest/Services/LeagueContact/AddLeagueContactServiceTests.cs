using Microsoft.AspNetCore.Mvc;

namespace pball.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactServiceSetup(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            Assert.NotNull(actionRes);
            Assert.NotNull(actionRes.Result);
            if (actionRes != null && actionRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                if (((OkObjectResult)actionRes.Result).Value != null)
                {
                    LeagueContact? leagueContactRet = (LeagueContact?)((OkObjectResult)actionRes.Result).Value;
                    Assert.NotNull(leagueContactRet);
                }
            }
        }
    }
}

