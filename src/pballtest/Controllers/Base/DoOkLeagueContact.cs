namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<LeagueContact?> DoOkLeagueContact(LeagueContact leagueContact, Contact contact, string culture)
    {
        LeagueContact? leagueContactRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                string stringData = JsonSerializer.Serialize(leagueContact);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                leagueContactRet = JsonSerializer.Deserialize<LeagueContact>(responseContent);
                Assert.NotNull(leagueContactRet);
                if (leagueContactRet != null)
                {
                    Assert.True(leagueContactRet.LeagueContactID > 0);
                }
            }
        }

        return await Task.FromResult(leagueContactRet);
    }
}

