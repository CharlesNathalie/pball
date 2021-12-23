namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueServiceSetup(culture));

        if (LeagueService != null)
        {
            League? leagueRet = new League();

            League league = await FillLeague();
            Assert.True(league.CreatorContactID > 0);

            var actionAddRes = await LeagueService.AddLeagueAsync(league);
            Assert.NotNull(actionAddRes);
            Assert.NotNull(actionAddRes.Result);
            if (actionAddRes != null && actionAddRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
                if (((OkObjectResult)actionAddRes.Result).Value != null)
                {
                    leagueRet = (League?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(leagueRet);
                }
            }

            if (leagueRet != null)
            {
                var actionDeleteRes = await LeagueService.DeleteLeagueAsync(leagueRet.LeagueID);
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
}


