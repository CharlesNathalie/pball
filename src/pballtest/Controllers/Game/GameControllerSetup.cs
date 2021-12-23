namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    public GameControllerTests() : base()
    {

    }

    private async Task<bool> GameControllerSetup(string culture)
    {
        await BaseControllerSetup(culture);

        return await Task.FromResult(true);
    }
}

