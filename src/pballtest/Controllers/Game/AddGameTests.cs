//namespace pball.Controllers.Tests;

//public partial class GameControllerTests : BaseControllerTests
//{
//    [Theory]
//    [InlineData("en-CA")]
//    [InlineData("fr-CA")]
//    public async Task AddGameAsync_Good_Test(string culture)
//    {
//        Assert.True(await GameControllerSetup(culture));

//        if (GameService != null)
//        {
//            Game game = await FillGame();
//            Assert.True(game.Team1Player1 > 0);
//            Assert.True(game.Team1Player2 > 0);
//            Assert.True(game.Team2Player1 > 0);
//            Assert.True(game.Team2Player2 > 0);
//            Assert.True(game.Team1Scores >= 0);
//            Assert.True(game.Team2Scores >= 0);

//            using (HttpClient httpClient = new HttpClient())
//            {
//                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
//                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

//                if (Configuration != null)
//                {
//                    string stringData = JsonSerializer.Serialize(game);
//                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
//                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
//                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

//                    string responseContent = await response.Content.ReadAsStringAsync();
//                    Game? gameRet = JsonSerializer.Deserialize<Game>(responseContent);
//                    Assert.NotNull(gameRet);
//                }
//            }
//        }
//    }
//}

