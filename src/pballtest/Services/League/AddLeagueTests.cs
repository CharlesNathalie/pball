namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();
            Assert.NotEmpty(league.LeagueName);

            var actionRes = await LeagueService.AddLeagueAsync(league);
            League? leagueRet = await DoOKTestReturnLeagueAsync(actionRes);
            Assert.NotNull(leagueRet);
        }

        if (db != null)
        {
            League? league = (from c in db.Leagues
                              select c).FirstOrDefault();

            Assert.NotNull(league);

            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_LeagueName_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();
            Assert.NotEmpty(league.LeagueName);

            league.LeagueName = "";

            var actionRes = await LeagueService.AddLeagueAsync(league);
            bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueName"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_LeagueName_AlreadyTaken_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            League league = await FillLeague();
            Assert.NotEmpty(league.LeagueName);

            var actionRes = await LeagueService.AddLeagueAsync(league);
            League? leagueRet = await DoOKTestReturnLeagueAsync(actionRes);
            Assert.NotNull(leagueRet);

            actionRes = await LeagueService.AddLeagueAsync(league);
            bool? boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._AlreadyTaken, "LeagueName"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
}

