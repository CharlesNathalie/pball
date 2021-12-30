namespace pball.Services.Tests;

[Collection("Sequential")]
public partial class ContactServiceTests : BaseServiceTests
{
    public ContactServiceTests() : base()
    {

    }

    private async Task<bool> _ContactServiceSetupAsync(string culture)
    {
        await _BaseServiceSetupAsync(culture);

        return await Task.FromResult(true);
    }
}

