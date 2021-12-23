namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Register_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        using (HttpClient httpClient = new HttpClient())
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            RegisterModel registerModel = await FillRegisterModel();

            string stringData = JsonSerializer.Serialize(registerModel);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/register", contentData).Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            Contact? contact = JsonSerializer.Deserialize<Contact>(responseContent);
            Assert.NotNull(contact);
        }
    }
}

