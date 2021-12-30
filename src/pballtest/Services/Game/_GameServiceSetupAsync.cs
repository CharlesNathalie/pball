namespace pball.Services.Tests;

[Collection("Sequential")]
public partial class GameServiceTests : BaseServiceTests
{
    public GameServiceTests() : base()
    {

    }

    private async Task<bool> _GameServiceSetupAsync(string culture)
    {
        await _BaseServiceSetupAsync(culture);

        return await Task.FromResult(true);
    }
}

