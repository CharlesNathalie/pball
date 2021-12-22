namespace pball.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    public LeagueContactServiceTests() : base()
    {

    }

    private async Task<bool> LeagueContactServiceSetup(string culture)
    {
        await BaseServiceSetup(culture);

        return await Task.FromResult(true);
    }
}

