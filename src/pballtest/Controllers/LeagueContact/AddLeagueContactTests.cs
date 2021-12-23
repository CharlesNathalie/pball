namespace pball.Controllers.Tests;

public partial class LeagueContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

        using (HttpClient httpClient = new HttpClient())
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            if (Configuration != null)
            {
                string stringData = JsonSerializer.Serialize(leagueContact);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                LeagueContact? leagueContactRet = JsonSerializer.Deserialize<LeagueContact>(responseContent);
                Assert.NotNull(leagueContactRet);
            }
        }
    }
}

