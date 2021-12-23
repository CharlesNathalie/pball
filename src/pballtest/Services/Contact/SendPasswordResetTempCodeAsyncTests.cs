namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task SendPasswordResetTempCodeAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await ContactServiceSetup(culture));

        int ContactID = 0;

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionAddRes = await ContactService.RegisterAsync(registerModel);
            Assert.NotNull(actionAddRes);
            Assert.NotNull(actionAddRes.Result);
            if (actionAddRes != null && actionAddRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
                if (((OkObjectResult)actionAddRes.Result).Value != null)
                {
                    Contact? contactRet = (Contact?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(contactRet);
                    if (contactRet != null)
                    {
                        ContactID = contactRet.ContactID;
                    }
                }
            }

            if (Configuration != null)
            {
                LoginEmailModel loginEmailModel = new LoginEmailModel()
                {
                    LoginEmail = registerModel.LoginEmail,
                };

                var actionRes = await ContactService.SendPasswordResetTempCodeAsync(loginEmailModel);
                Assert.NotNull(actionRes);
                Assert.NotNull(actionRes.Result);
                if (actionRes != null && actionRes.Result != null)
                {
                    Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                    Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                    if (((OkObjectResult)actionRes.Result).Value != null)
                    {
                        bool? boolRet = (bool?)((OkObjectResult)actionRes.Result).Value;
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}

