namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Good_Test(string culture)
    {
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

            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                if (Configuration != null)
                {
                    string stringData = JsonSerializer.Serialize(game);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Game? gameRet = JsonSerializer.Deserialize<Game>(responseContent);
                    Assert.NotNull(gameRet);
                }
            }
        }
    }
}

