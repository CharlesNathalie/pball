namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueControllerSetup(culture));

        List<League>? leagueList = await DoOkGetAllLeague(culture);
        Assert.NotNull(leagueList); 
        Assert.NotEmpty(leagueList);
    }
}

