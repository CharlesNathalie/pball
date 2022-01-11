namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<List<MostGamesPlayedModel>?> DoOkMostGamesPlayedBetweenDates(LeagueGamesModel leagueGamesModel, Contact contact, string culture)
    {
        List<MostGamesPlayedModel>? mostGamesPlayedModelListRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                string stringData = JsonSerializer.Serialize(leagueGamesModel);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/game/getmostgamesplayedbetweendates", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                mostGamesPlayedModelListRet = JsonSerializer.Deserialize<List<MostGamesPlayedModel>>(responseContent);
                Assert.NotNull(mostGamesPlayedModelListRet);
                if (mostGamesPlayedModelListRet != null)
                {
                    Assert.NotEmpty(mostGamesPlayedModelListRet);
                }
            }
        }

        return await Task.FromResult(mostGamesPlayedModelListRet);
    }
}

