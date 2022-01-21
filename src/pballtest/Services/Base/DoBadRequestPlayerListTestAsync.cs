namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<bool> DoBadRequestPlayerListTestAsync(string ErrMessage, ActionResult<List<Player>> actionRes)
    {
        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(400, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((BadRequestObjectResult)actionRes.Result).Value);
            if (((BadRequestObjectResult)actionRes.Result).Value != null)
            {
                ErrRes? errRes = (ErrRes?)((BadRequestObjectResult)actionRes.Result).Value;
                Assert.NotNull(errRes);
                if (errRes != null)
                {
                    Assert.NotEmpty(errRes.ErrList);
                    Assert.Equal(ErrMessage, errRes.ErrList[0]);
                }
            }
        }

        return await Task.FromResult(true);
    }
}

