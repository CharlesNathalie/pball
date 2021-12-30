namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<string> DoOKTestReturnStringAsync(ActionResult<string> actionRes)
    {
        string? strRet = "";

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                strRet = (string?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(strRet);
            }
        }

        return await Task.FromResult(strRet ?? "");
    }
}

