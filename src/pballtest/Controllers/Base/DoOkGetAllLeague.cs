namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<List<League>?> DoOkGetAllLeague(string culture)
    {
        List<League>? leagueListRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/league/getallleagues").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                leagueListRet = JsonSerializer.Deserialize<List<League>>(responseContent);
                Assert.NotNull(leagueListRet);
                if (leagueListRet != null)
                {
                    Assert.NotEmpty(leagueListRet);
                }
            }
        }

        return await Task.FromResult(leagueListRet);
    }
}

