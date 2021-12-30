namespace pball.Services.Tests;

[Collection("Sequential")]
public partial class ScrambleServiceTests : BaseServiceTests
{
    public ScrambleServiceTests() : base()
    {

    }

    private async Task<bool> _ScrambleServiceSetupAsync(string culture)
    {
        await _BaseServiceSetupAsync(culture);

        return await Task.FromResult(true);
    }
}

