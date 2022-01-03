namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<Game?> DoOkGame(Game game, Contact contact, string culture)
    {
        Game? gameRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                string stringData = JsonSerializer.Serialize(game);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                gameRet = JsonSerializer.Deserialize<Game>(responseContent);
                Assert.NotNull(gameRet);
                if (gameRet != null)
                {
                    Assert.True(gameRet.LeagueID > 0);
                }
            }
        }

        return await Task.FromResult(gameRet);
    }
}

