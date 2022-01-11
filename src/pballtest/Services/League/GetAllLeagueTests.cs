namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

        if (LeagueService != null)
        {
            var actionRes = await LeagueService.GetAllLeaguesAsync();
            List<League>? leagueListRet = await DoOKTestReturnLeagueListAsync(actionRes);
            Assert.NotNull(leagueListRet);
            Assert.NotEmpty(leagueListRet);
        }
    }
}

