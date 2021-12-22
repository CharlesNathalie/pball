namespace pball.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Login_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        using (HttpClient httpClient = new HttpClient())
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = Configuration?["LoginEmail"],
                Password = Configuration?["Password"],
            };

            string stringData = JsonSerializer.Serialize(loginModel);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/register", contentData).Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            Contact? contact = JsonSerializer.Deserialize<Contact>(responseContent);
            Assert.NotNull(contact);
        }
    }
}

