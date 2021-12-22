namespace pball.Tests;

public partial class LoggedInServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task SetLoggedInLocalContactInf_Good_Test(string culture)
    {
        Assert.True(await LoggedInServiceSetup(culture));

        if (LoggedInService != null)
        {
            if (Configuration != null)
            {
                await LoggedInService.SetLoggedInContactInfoAsync(Configuration["LoginEmail"]);
                Assert.NotNull(LoggedInService.LoggedInContactInfo.LoggedInContact);
            }
       }
    }
}

