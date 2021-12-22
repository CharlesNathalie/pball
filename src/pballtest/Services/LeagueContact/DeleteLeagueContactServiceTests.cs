using Microsoft.AspNetCore.Mvc;

namespace pball.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactServiceSetup(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            var actionAddRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            Assert.NotNull(actionAddRes);
            Assert.NotNull(actionAddRes.Result);
            if (actionAddRes != null && actionAddRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
                if (((OkObjectResult)actionAddRes.Result).Value != null)
                {
                    LeagueContact? leagueContactRet = (LeagueContact?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(leagueContactRet);
                }
            }

            var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContact.LeagueContactID);
            Assert.NotNull(actionDeleteRes);
            Assert.NotNull(actionDeleteRes.Result);
            if (actionDeleteRes != null && actionDeleteRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionDeleteRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionDeleteRes.Result).Value);
                if (((OkObjectResult)actionDeleteRes.Result).Value != null)
                {
                    bool? boolRet = (bool?)((OkObjectResult)actionDeleteRes.Result).Value;
                    Assert.NotNull(boolRet);
                }
            }
        }
    }
}

