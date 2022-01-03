namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<League?> DoOkLeague(League league, Contact contact, string culture)
    {
        League? leagueRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                string stringData = JsonSerializer.Serialize(league);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/league", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                leagueRet = JsonSerializer.Deserialize<League>(responseContent);
                Assert.NotNull(leagueRet);
                if (leagueRet != null)
                {
                    Assert.True(leagueRet.LeagueID > 0);
                }
            }
        }

        return await Task.FromResult(leagueRet);
    }
}

