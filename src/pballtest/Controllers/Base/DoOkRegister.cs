namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<Contact?> DoOkRegister(RegisterModel registerModel, string culture)
    {
        Contact? contact = new Contact();

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                string stringData = JsonSerializer.Serialize(registerModel);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/register", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                contact = JsonSerializer.Deserialize<Contact>(responseContent);
                Assert.NotNull(contact);
            }
        }

        return await Task.FromResult(contact);
    }
}

