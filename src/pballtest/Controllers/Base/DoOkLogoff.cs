namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<bool?> DoOkLogoff(Contact contact, string culture)
    {
        bool? boolRet = null;

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/logoff/{ contact.ContactID }").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                Assert.NotNull(boolRet);
            }
        }

        return await Task.FromResult(boolRet);
    }
}

