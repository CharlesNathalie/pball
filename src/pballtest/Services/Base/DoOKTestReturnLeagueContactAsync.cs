namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<LeagueContact?> DoOKTestReturnLeagueContactAsync(ActionResult<LeagueContact> actionRes)
    {
        LeagueContact? leagueContact = new LeagueContact();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                leagueContact = (LeagueContact?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(leagueContact);
            }
        }

        return await Task.FromResult(leagueContact);
    }
}

