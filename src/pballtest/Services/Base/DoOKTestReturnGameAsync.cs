namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<Game?> DoOKTestReturnGameAsync(ActionResult<Game> actionRes)
    {
        Game? game = new Game();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                game = (Game?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(game);
            }
        }

        return await Task.FromResult(game);
    }
}

