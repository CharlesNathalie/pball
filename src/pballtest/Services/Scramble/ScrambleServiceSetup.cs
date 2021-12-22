namespace pball.Tests;


public partial class ScrambleServiceTests : BaseServiceTests
{
    public ScrambleServiceTests() : base()
    {

    }

    private async Task<bool> ScrambleServiceSetup(string culture)
    {
        await BaseServiceSetup(culture);

        return await Task.FromResult(true);
    }
}

