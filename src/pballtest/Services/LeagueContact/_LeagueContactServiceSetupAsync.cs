namespace pball.Services.Tests;

[Collection("Sequential")]
public partial class LeagueContactServiceTests : BaseServiceTests
{
    public LeagueContactServiceTests() : base()
    {

    }

    private async Task<bool> _LeagueContactServiceSetupAsync(string culture)
    {
        await _BaseServiceSetupAsync(culture);

        return await Task.FromResult(true);
    }
}

