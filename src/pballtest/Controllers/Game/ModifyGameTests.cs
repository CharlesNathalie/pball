namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyGameAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await GameControllerSetup(culture));

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

            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                if (Configuration != null)
                {
                    string stringData = JsonSerializer.Serialize(game);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Game? gameRet = JsonSerializer.Deserialize<Game>(responseContent);
                    Assert.NotNull(gameRet);
                }
            }
        }
    }
}

