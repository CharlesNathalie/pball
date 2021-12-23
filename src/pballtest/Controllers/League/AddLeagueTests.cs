namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueControllerSetup(culture));

        if (LeagueService != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                League league = await FillLeague();
                Assert.True(league.CreatorContactID > 0);

                if (Configuration != null)
                {
                    string stringData = JsonSerializer.Serialize(league);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/league", contentData).Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    League? leagueRet = JsonSerializer.Deserialize<League>(responseContent);
                    Assert.NotNull(leagueRet);
                }
            }
        }
    }
}

