namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<bool> ClearServerLoggedInListAsync(string culture)
    {
        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/clearserverloggedinlist").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }

        return await Task.FromResult(true);
    }
}

