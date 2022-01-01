//namespace pball.Controllers.Tests;

//public partial class GameControllerTests : BaseControllerTests
//{
//    [Theory]
//    [InlineData("en-CA")]
//    [InlineData("fr-CA")]
//    public async Task DeleteGameAsync_Good_Test(string culture)
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

//            var actionAddRes = await GameService.AddGameAsync(game);
//            Assert.NotNull(actionAddRes);
//            Assert.NotNull(actionAddRes.Result);
//            if (actionAddRes != null && actionAddRes.Result != null)
//            {
//                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
//                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
//                if (((OkObjectResult)actionAddRes.Result).Value != null)
//                {
//                    Game? gameRet = (Game?)((OkObjectResult)actionAddRes.Result).Value;
//                    Assert.NotNull(gameRet);
//                }
//            }

//            using (HttpClient httpClient = new HttpClient())
//            {
//                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
//                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

//                if (Configuration != null)
//                {
//                    HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/game/{ game.GameID }").Result;
//                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

//                    string responseContent = await response.Content.ReadAsStringAsync();
//                    bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
//                    Assert.True(boolRet);
//                }

//                if (Configuration != null)
//                {
//                    HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/game/{ -1 }").Result;
//                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

//                    string responseContent = await response.Content.ReadAsStringAsync();
//                    bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
//                    Assert.False(boolRet);
//                }
//            }
//        }
//    }
//}
