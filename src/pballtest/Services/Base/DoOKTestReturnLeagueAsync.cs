namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<League?> DoOKTestReturnLeagueAsync(ActionResult<League> actionRes)
    {
        League? league = new League();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                league = (League?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(league);
            }
        }

        return await Task.FromResult(league);
    }
}

