namespace pball.Services.Tests;

[Collection("Sequential")]
public partial class LoggedInServiceTests : BaseServiceTests
{
    public LoggedInServiceTests() : base()
    {

    }

    private async Task<bool> _LoggedInServiceSetupAsync(string culture)
    {
        await _BaseServiceSetupAsync(culture);

        return await Task.FromResult(true);
    }
}

