namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Good_Test(string culture)
    {
        Assert.True(await ContactServiceSetup(culture));

        if (ContactService != null)
        {
            if (Configuration != null)
            {
                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = Configuration["Password"],
                };

                var actionRes = await ContactService.LoginAsync(loginModel);
                Assert.NotNull(actionRes);
                Assert.NotNull(actionRes.Result);
                if (actionRes != null && actionRes.Result != null)
                {
                    Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                    Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                    if (((OkObjectResult)actionRes.Result).Value != null)
                    {
                        Contact? contactRet = (Contact?)((OkObjectResult)actionRes.Result).Value;
                        Assert.NotNull(contactRet);
                    }
                }
            }
        }
    }
}

