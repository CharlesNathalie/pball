namespace pball.Controllers.Tests;


[Collection("Sequential")]
public partial class ContactControllerTests : BaseControllerTests
{
    public ContactControllerTests() : base()
    {

    }

    private async Task<bool> ContactControllerSetup(string culture)
    {
        await BaseControllerSetup(culture);

        return await Task.FromResult(true);
    }
}

