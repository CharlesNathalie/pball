namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Constructor_Good_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));
    }
}

