namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<List<LeagueContact>?> DoOkGetLeagueContacts(int LeagueID, string culture)
    {
        List<LeagueContact>? leagueContactListRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                //string stringData = JsonSerializer.Serialize(leagueContact);
                //var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact/getleaguecontacts/{ LeagueID }").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                leagueContactListRet = JsonSerializer.Deserialize<List<LeagueContact>>(responseContent);
                Assert.NotNull(leagueContactListRet);
                if (leagueContactListRet != null)
                {
                    Assert.NotEmpty(leagueContactListRet);
                }
            }
        }

        return await Task.FromResult(leagueContactListRet);
    }
}

