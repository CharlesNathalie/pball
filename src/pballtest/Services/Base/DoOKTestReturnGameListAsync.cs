namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<List<Game>?> DoOKTestReturnGameListAsync(ActionResult<List<Game>> actionRes)
    {
        List<Game>? gameList = new List<Game>();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                gameList = (List<Game>?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(gameList);
            }
        }

        return await Task.FromResult(gameList);
    }
}

