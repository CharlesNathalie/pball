namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Constructor_Good_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));
    }
}

