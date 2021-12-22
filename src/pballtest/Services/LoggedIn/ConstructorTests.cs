namespace pball.Tests;

public partial class LoggedInServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Constructor_Good_Test(string culture)
    {
        Assert.True(await LoggedInServiceSetup(culture));
    }
}

