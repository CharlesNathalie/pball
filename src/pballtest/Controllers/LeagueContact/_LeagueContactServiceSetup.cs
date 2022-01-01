namespace pball.Controllers.Tests;


[Collection("Sequential")]
public partial class LeagueContactControllerTests : BaseControllerTests
{
    public LeagueContactControllerTests() : base()
    {

    }

    private async Task<bool> LeagueContactControllerSetup(string culture)
    {
        await BaseControllerSetup(culture);

        return await Task.FromResult(true);
    }
}

