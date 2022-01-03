namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<ErrRes?> DoBadRequestLogoff(Contact contact, string culture)
    {
        ErrRes? errRes = new ErrRes();

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/logoff/{ contact.ContactID }").Result;
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                Assert.NotNull(errRes);
            }
        }

        return await Task.FromResult(errRes);
    }
}

