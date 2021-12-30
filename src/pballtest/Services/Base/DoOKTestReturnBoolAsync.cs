namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<bool?> DoOKTestReturnBoolAsync(ActionResult<bool> actionRes)
    {
        bool? boolRet = null;

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                boolRet = (bool?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(boolRet);
            }
        }

        return await Task.FromResult(boolRet);
    }
}

