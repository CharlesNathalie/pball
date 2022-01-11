namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<List<MostGamesPlayedModel>?> DoOKTestReturnMostGamePlayedModelListAsync(ActionResult<List<MostGamesPlayedModel>> actionRes)
    {
        List<MostGamesPlayedModel>? mostGamePlayedList = new List<MostGamesPlayedModel>();

        Assert.NotNull(actionRes);
        Assert.NotNull(actionRes.Result);
        if (actionRes != null && actionRes.Result != null)
        {
            Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
            Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
            if (((OkObjectResult)actionRes.Result).Value != null)
            {
                mostGamePlayedList = (List<MostGamesPlayedModel>?)((OkObjectResult)actionRes.Result).Value;
                Assert.NotNull(mostGamePlayedList);
            }
        }

        return await Task.FromResult(mostGamePlayedList);
    }
}

