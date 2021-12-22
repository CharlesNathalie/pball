namespace pball.Tests;

public partial class ScrambleServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Constructor_Good_Test(string culture)
    {
        Assert.True(await ScrambleServiceSetup(culture));
    }
}

