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
            Assert.True(game.Team1Player1 > 0);
            Assert.True(game.Team1Player2 > 0);
            Assert.True(game.Team2Player1 > 0);
            Assert.True(game.Team2Player2 > 0);
            Assert.True(game.Team1Scores >= 0);
            Assert.True(game.Team2Scores >= 0);

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

            game.Team1Scores = random.Next(1, 11);
            game.Team2Scores = random.Next(1, 11);

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

