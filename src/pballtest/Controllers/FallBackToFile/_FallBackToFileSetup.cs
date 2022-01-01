namespace pball.Controllers.Tests;


[Collection("Sequential")]
public partial class FallBackToFileControllerTests : BaseControllerTests
{
    public FallBackToFileControllerTests() : base()
    {

    }

    private async Task<bool> FallBackToFileControllerSetup(string culture)
    {
        await BaseControllerSetup(culture);

        return await Task.FromResult(true);
    }
}

