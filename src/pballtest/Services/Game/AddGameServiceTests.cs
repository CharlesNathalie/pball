using Microsoft.AspNetCore.Mvc;

namespace pball.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Good_Test(string culture)
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

            var actionRes = await GameService.AddGameAsync(game);
            Assert.NotNull(actionRes);
            Assert.NotNull(actionRes.Result);
            if (actionRes != null && actionRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                if (((OkObjectResult)actionRes.Result).Value != null)
                {
                    Game? gameRet = (Game?)((OkObjectResult)actionRes.Result).Value;
                    Assert.NotNull(gameRet);
                }
            }
        }
    }
}

