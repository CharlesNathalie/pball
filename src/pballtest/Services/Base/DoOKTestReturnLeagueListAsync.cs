namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<List<League>?> DoOKTestReturnLeagueListAsync(ActionResult<List<League>> actionRes)
    {
        List<League>? leagueList = new List<League>();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                leagueList = (List<League>?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(leagueList);
            }
        }

        return await Task.FromResult(leagueList);
    }
}

