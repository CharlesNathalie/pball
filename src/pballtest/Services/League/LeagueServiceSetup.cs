namespace pball.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    public LeagueServiceTests() : base()
    {

    }

    private async Task<bool> LeagueServiceSetup(string culture)
    {
        await BaseServiceSetup(culture);

        return await Task.FromResult(true);
    }
}

