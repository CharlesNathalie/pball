namespace pball.Tests;


public partial class LoggedInServiceTests : BaseServiceTests
{
    public LoggedInServiceTests() : base()
    {

    }

    private async Task<bool> LoggedInServiceSetup(string culture)
    {
        await BaseServiceSetup(culture);

        return await Task.FromResult(true);
    }
}

