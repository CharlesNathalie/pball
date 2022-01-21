namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<List<Player>?> DoOKTestReturnPlayerListAsync(ActionResult<List<Player>> actionRes)
    {
        List<Player>? PlayerList = new List<Player>();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                PlayerList = (List<Player>?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(PlayerList);
            }
        }

        return await Task.FromResult(PlayerList);
    }
}

