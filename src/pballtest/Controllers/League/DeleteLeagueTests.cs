namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueControllerSetup(culture));

        if (LeagueService != null)
        {
            League? leagueRet = new League();

            League league = await FillLeague();
            Assert.NotEmpty(league.LeagueName);

            var actionAddRes = await LeagueService.AddLeagueAsync(league);
            Assert.NotNull(actionAddRes);
            Assert.NotNull(actionAddRes.Result);
            if (actionAddRes != null && actionAddRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
                if (((OkObjectResult)actionAddRes.Result).Value != null)
                {
                    leagueRet = (League?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(leagueRet);
                }
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                if (Configuration != null)
                {
                    if (leagueRet != null)
                    {
                        HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/league/{ leagueRet.LeagueID }").Result;
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                        string responseContent = await response.Content.ReadAsStringAsync();
                        bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}

