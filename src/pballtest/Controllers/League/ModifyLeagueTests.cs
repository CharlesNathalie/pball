namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await LeagueControllerSetup(culture));

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

            league.LeagueName = $"League Name { random.Next(1, 1000) }";
          

            var actionModifyRes = await LeagueService.AddLeagueAsync(league);
            Assert.NotNull(actionModifyRes);
            Assert.NotNull(actionModifyRes.Result);
            if (actionModifyRes != null && actionModifyRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionModifyRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionModifyRes.Result).Value);
                if (((OkObjectResult)actionModifyRes.Result).Value != null)
                {
                    League? leagueRet = (League?)((OkObjectResult)actionModifyRes.Result).Value;
                    Assert.NotNull(leagueRet);
                    if (leagueRet != null)
                    {
                        Assert.Equal(league.LeagueName, leagueRet.LeagueName);
                    }
                }
            }
        }
    }
}

