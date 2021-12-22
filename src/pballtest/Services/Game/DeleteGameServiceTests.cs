using Microsoft.AspNetCore.Mvc;

namespace pball.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteGameAsync_Good_Test(string culture)
    {
        Assert.True(await GameServiceSetup(culture));

        if (GameService != null)
        {
            Game game = await FillGame();
            Assert.True(game.Player1 > 0);
            Assert.True(game.Player2 > 0);
            Assert.True(game.Player3 > 0);
            Assert.True(game.Player4 > 0);
            Assert.True(game.Scores1 >= 0);
            Assert.True(game.Scores2 >= 0);
            Assert.True(game.Scores3 >= 0);
            Assert.True(game.Scores4 >= 0);

            var actionAddRes = await GameService.AddGameAsync(game);
            Assert.NotNull(actionAddRes);
            Assert.NotNull(actionAddRes.Result);
            if (actionAddRes != null && actionAddRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
                if (((OkObjectResult)actionAddRes.Result).Value != null)
                {
                    Game? gameRet = (Game?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(gameRet);
                }
            }

            var actionDeleteRes = await GameService.DeleteGameAsync(game.GameID);
            Assert.NotNull(actionDeleteRes);
            Assert.NotNull(actionDeleteRes.Result);
            if (actionDeleteRes != null && actionDeleteRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionDeleteRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionDeleteRes.Result).Value);
                if (((OkObjectResult)actionDeleteRes.Result).Value != null)
                {
                    bool? boolRet = (bool?)((OkObjectResult)actionDeleteRes.Result).Value;
                    Assert.NotNull(boolRet);
                }
            }
        }
    }
}

