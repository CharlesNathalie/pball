using Microsoft.AspNetCore.Mvc;

namespace pball.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueServiceSetup(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();
            Assert.True(league.CreatorContactID > 0);

            var actionRes = await LeagueService.AddLeagueAsync(league);
            Assert.NotNull(actionRes);
            Assert.NotNull(actionRes.Result);
            if (actionRes != null && actionRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                if (((OkObjectResult)actionRes.Result).Value != null)
                {
                    League? leagueRet = (League?)((OkObjectResult)actionRes.Result).Value;
                    Assert.NotNull(leagueRet);
                }
            }
        }
    }
}

