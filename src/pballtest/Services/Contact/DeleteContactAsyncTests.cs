namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteContactAsync_Good_Test(string culture)
    {
        Assert.True(await ContactServiceSetup(culture));

        if (ContactService != null)
        {
            int ContactIDToDelete = 0;

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
                        ContactIDToDelete = contactRet.ContactID;
                    }
                }
            }

            var actionDeleteRes = await ContactService.DeleteContactAsync(ContactIDToDelete);
            Assert.NotNull(actionDeleteRes);
            Assert.NotNull(actionDeleteRes.Result);
            if (actionDeleteRes != null && actionDeleteRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionDeleteRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionDeleteRes.Result).Value);
                if (((OkObjectResult)actionDeleteRes.Result).Value != null)
                {
                    bool? boolRet = (bool?)((OkObjectResult)actionDeleteRes.Result).Value;
                    Assert.NotNull(boolRet);
                }
            }
        }
    }
}

