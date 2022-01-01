namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<ErrRes?> DoBadRequestRegister(RegisterModel registerModel, string culture)
    {
        ErrRes? errRes = new ErrRes();

        using (HttpClient httpClient = new HttpClient())
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            string stringData = JsonSerializer.Serialize(registerModel);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/register", contentData).Result;
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
            Assert.NotNull(errRes);
        }

        return await Task.FromResult(errRes);
    }
}

