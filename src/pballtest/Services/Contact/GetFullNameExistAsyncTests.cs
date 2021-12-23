namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetFullNameExistAsync_Good_Test(string culture)
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
                FullNameModel fullNameModel = new FullNameModel()
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Initial = registerModel.Initial,
                };

                var actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
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

                fullNameModel.FirstName = $" { registerModel.FirstName}Not";

                var actionRes2 = await ContactService.GetFullNameExistAsync(fullNameModel);
                Assert.NotNull(actionRes2);
                Assert.NotNull(actionRes2.Result);
                if (actionRes2 != null && actionRes2.Result != null)
                {
                    Assert.Equal(200, ((ObjectResult)actionRes2.Result).StatusCode);
                    Assert.NotNull(((OkObjectResult)actionRes2.Result).Value);
                    if (((OkObjectResult)actionRes2.Result).Value != null)
                    {
                        bool? boolRet = (bool?)((OkObjectResult)actionRes2.Result).Value;
                        Assert.NotNull(boolRet);
                        Assert.False(boolRet);
                    }
                }
            }
        }
    }
}

