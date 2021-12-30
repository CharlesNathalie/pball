namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<bool> DoRegisterTestAsync()
    {
        if (Configuration != null)
        {
            RegisterModel registerModel = new RegisterModel()
            {
                FirstName = "Charles",
                LastName = "LeBlanc",
                Initial = "",
                LoginEmail = Configuration["LoginEmail"],
                Password = Configuration["Password"],
                PlayerLevel = 3.0f,
            };

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
                        Contact? contactRet = (Contact?)((OkObjectResult)actionAddRes.Result).Value;
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

        return await Task.FromResult(true);
    }
}

