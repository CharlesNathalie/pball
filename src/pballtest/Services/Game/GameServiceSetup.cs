namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    public GameServiceTests() : base()
    {

    }

    private async Task<bool> GameServiceSetup(string culture)
    {
        await BaseServiceSetup(culture);

        return await Task.FromResult(true);
    }
}

