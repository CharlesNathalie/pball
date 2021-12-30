namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();
            Assert.NotEmpty(league.LeagueName);

            var actionRes = await LeagueService.AddLeagueAsync(league);
            League? leagueRet = await DoOKTestReturnLeagueAsync(actionRes);
            Assert.NotNull(leagueRet);

            if (leagueRet != null)
            {
                var actionDeleteRes = await LeagueService.DeleteLeagueAsync(leagueRet.LeagueID);
                League? leagueRet2 = await DoOKTestReturnLeagueAsync(actionDeleteRes);
                Assert.NotNull(leagueRet2);
                if (leagueRet2 != null)
                {
                    Assert.True(leagueRet2.LeagueID > 0);
                    Assert.True(leagueRet2.Removed);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactInfo.LoggedInContact = null;
        }

        if (LeagueService != null)
        {
            var actionRes = await LeagueService.AddLeagueAsync(new League());
            bool? boolRet = await DoBadRequestLeagueTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();

            league.LeagueID = 0;

            var actionRes = await LeagueService.DeleteLeagueAsync(league.LeagueID);
            bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            league.LeagueID = 10000;

            actionRes = await LeagueService.DeleteLeagueAsync(league.LeagueID);
            boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", league.LeagueID.ToString()), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
}


