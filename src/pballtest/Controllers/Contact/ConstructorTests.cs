namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    //[InlineData("fr-CA")]
    public async Task Constructor_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));
    }
}

