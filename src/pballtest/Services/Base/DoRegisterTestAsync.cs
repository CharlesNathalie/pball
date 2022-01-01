namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<Contact?> DoRegisterTestAsync(RegisterModel registerModel)
    {
        Random random = new Random();

        Contact? contactRet = null;

        if (Configuration != null)
        {

            if (ContactService != null)
            {
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
                        if (contactRet != null)
                        {
                            Assert.Empty(contactRet.PasswordHash);
                            Assert.Empty(contactRet.Token);
                        }
                    }
                }
            }
        }

        return await Task.FromResult(contactRet);
    }
}

