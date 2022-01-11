namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<List<Contact>?> DoOkGetAllContactsForLeague(int LeagueID, string culture)
    {
        List<Contact>? contactListRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getallcontactsforleague/{ LeagueID }").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                contactListRet = JsonSerializer.Deserialize<List<Contact>>(responseContent);
                Assert.NotNull(contactListRet);
                if (contactListRet != null)
                {
                    Assert.NotEmpty(contactListRet);
                }
            }
        }

        return await Task.FromResult(contactListRet);
    }
}

