namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_Good_Test(string culture)
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
                leagueRet.LeagueName = league.LeagueName + "new";
                leagueRet.PercentPointsFactor = 1.2D;
                leagueRet.PointsToLoosers = 1.3D;
                leagueRet.PointsToWinners = 4.3D;
                leagueRet.PlayerLevelFactor = 2.1D;

                var actionModifyRes = await LeagueService.ModifyLeagueAsync(leagueRet);
                League? leagueRet2 = await DoOKTestReturnLeagueAsync(actionModifyRes);
                Assert.NotNull(leagueRet2);
                if (leagueRet2 != null)
                {
                    Assert.Equal(leagueRet.LeagueID, leagueRet2.LeagueID);
                    Assert.Equal(leagueRet.LeagueName, leagueRet2.LeagueName);
                    Assert.Equal(leagueRet.PercentPointsFactor, leagueRet2.PercentPointsFactor);
                    Assert.Equal(leagueRet.PointsToLoosers, leagueRet2.PointsToLoosers);
                    Assert.Equal(leagueRet.PointsToWinners, leagueRet2.PointsToWinners);
                    Assert.Equal(leagueRet.PlayerLevelFactor, leagueRet2.PlayerLevelFactor);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactInfo.LoggedInContact = null;
        }

        if (LeagueService != null)
        {
            var actionModifyRes = await LeagueService.ModifyLeagueAsync(new League());
            bool? boolRet = await DoBadRequestLeagueTestAsync(PBallRes.YouDoNotHaveAuthorization, actionModifyRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();

            league.LeagueID = 0;

            var actionModifyRes = await LeagueService.ModifyLeagueAsync(league);
            bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionModifyRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            league.LeagueID = 10000;

            actionModifyRes = await LeagueService.ModifyLeagueAsync(league);
            boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", league.LeagueID.ToString()), actionModifyRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_LeagueName_Required_Error_Test(string culture)
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
                leagueRet.LeagueName = "";

                var actionModifyRes = await LeagueService.ModifyLeagueAsync(leagueRet);
                bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueName"), actionModifyRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_LeagueName_AlreadyTaken_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();
            Assert.NotEmpty(league.LeagueName);

            var actionRes = await LeagueService.AddLeagueAsync(league);
            League? leagueRet = await DoOKTestReturnLeagueAsync(actionRes);
            Assert.NotNull(leagueRet);

            League league2 = await FillLeague();
            Assert.NotEmpty(league2.LeagueName);

            league2.LeagueName = league2.LeagueName + "new";

            var actionRes2 = await LeagueService.AddLeagueAsync(league2);
            League? leagueRet2 = await DoOKTestReturnLeagueAsync(actionRes2);
            Assert.NotNull(leagueRet2);

            if (leagueRet != null)
            {
                leagueRet.LeagueName = league2.LeagueName;

                var actionModifyRes = await LeagueService.ModifyLeagueAsync(leagueRet);
                bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._AlreadyTaken, "LeagueName"), actionModifyRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

