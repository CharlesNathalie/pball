namespace pball.Controllers.Tests;


[Collection("Sequential")]
public partial class LeagueControllerTests : BaseControllerTests
{
    public LeagueControllerTests() : base()
    {

    }

    private async Task<bool> LeagueControllerSetup(string culture)
    {
        await BaseControllerSetup(culture);

        return await Task.FromResult(true);
    }
}

