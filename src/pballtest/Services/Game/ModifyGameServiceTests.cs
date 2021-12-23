namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyGameAsync_Good_Test(string culture)
    {
        Random random = new Random();

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

            game.Scores1 = random.Next(1, 11);
            game.Scores3 = random.Next(1, 11);

            var actionModifyRes = await GameService.ModifyGameAsync(game);
            Assert.NotNull(actionModifyRes);
            Assert.NotNull(actionModifyRes.Result);
            if (actionModifyRes != null && actionModifyRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionModifyRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionModifyRes.Result).Value);
                if (((OkObjectResult)actionModifyRes.Result).Value != null)
                {
                    Game? gameRet = (Game?)((OkObjectResult)actionModifyRes.Result).Value;
                    Assert.NotNull(gameRet);
                    if (gameRet != null)
                    {
                        Assert.Equal(game.Scores1, gameRet.Scores1);
                        Assert.Equal(game.Scores3, gameRet.Scores3);
                    }
                }
            }
        }
    }
}

