namespace pball.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    public ContactServiceTests() : base()
    {

    }

    private async Task<bool> ContactServiceSetup(string culture)
    {
        await BaseServiceSetup(culture);

        return await Task.FromResult(true);
    }
}

