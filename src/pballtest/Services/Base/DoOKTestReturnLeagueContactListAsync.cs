namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<List<LeagueContact>?> DoOKTestReturnLeagueContactListAsync(ActionResult<List<LeagueContact>> actionRes)
    {
        List<LeagueContact>? leagueContactList = new List<LeagueContact>();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                leagueContactList = (List<LeagueContact>?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(leagueContactList);
            }
        }

        return await Task.FromResult(leagueContactList);
    }
}

