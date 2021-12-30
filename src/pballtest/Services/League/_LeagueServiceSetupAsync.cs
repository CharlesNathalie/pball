namespace pball.Services.Tests;

[Collection("Sequential")]
public partial class LeagueServiceTests : BaseServiceTests
{
    public LeagueServiceTests() : base()
    {

    }

    private async Task<bool> _LeagueServiceSetupAsync(string culture)
    {
        await _BaseServiceSetupAsync(culture);

        return await Task.FromResult(true);
    }
}

