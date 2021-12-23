namespace pball.Controllers.Tests;

public partial class LeagueContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

        if (LeagueContactService != null)
        {
            LeagueContact? leagueContactRet = new LeagueContact();

            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            var actionAddRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            Assert.NotNull(actionAddRes);
            Assert.NotNull(actionAddRes.Result);
            if (actionAddRes != null && actionAddRes.Result != null)
            {
                Assert.Equal(200, ((ObjectResult)actionAddRes.Result).StatusCode);
                Assert.NotNull(((OkObjectResult)actionAddRes.Result).Value);
                if (((OkObjectResult)actionAddRes.Result).Value != null)
                {
                    leagueContactRet = (LeagueContact?)((OkObjectResult)actionAddRes.Result).Value;
                    Assert.NotNull(leagueContactRet);
                }
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                if (Configuration != null)
                {
                    HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact/{ leagueContactRet }").Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                    Assert.True(boolRet);
                }

                if (Configuration != null)
                {
                    HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact/{ -1 }").Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                    Assert.False(boolRet);
                }
            }
        }
    }
}

