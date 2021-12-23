namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await ContactServiceSetup(culture));

        if (ContactService != null)
        {
            Contact? contactRet = new Contact();

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
                    contactRet = (Contact?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(contactRet);
                }
            }

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.FirstName = "LeBlancNew";
                contactRet.FirstName = "GNew";

                if (Configuration != null)
                {
                    var actionRes = await ContactService.ModifyContactAsync(contactRet);
                    Assert.NotNull(actionRes);
                    Assert.NotNull(actionRes.Result);
                    if (actionRes != null && actionRes.Result != null)
                    {
                        Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                        Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                        if (((OkObjectResult)actionRes.Result).Value != null)
                        {
                            Contact? contactRet2 = (Contact?)((OkObjectResult)actionRes.Result).Value;
                            Assert.NotNull(contactRet2);
                            if (contactRet2 != null)
                            {
                                Assert.Equal(contactRet.FirstName, contactRet2.FirstName);
                                Assert.Equal(contactRet.LastName, contactRet2.LastName);
                                Assert.Equal(contactRet.Initial, contactRet2.Initial);
                            }
                        }
                    }
                }
            }
        }
    }
}

