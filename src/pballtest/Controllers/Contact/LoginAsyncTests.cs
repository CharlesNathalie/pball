namespace pball.Tests;

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
            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/login", contentData).Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            Contact? contact = JsonSerializer.Deserialize<Contact>(responseContent);
            Assert.NotNull(contact);
        }
    }

    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Register_Error_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        List<LoginModel> loginModelList = new List<LoginModel>()
        {
            new LoginModel() { LoginEmail = "WillError", Password = Configuration?["Password"] },
            new LoginModel() { LoginEmail = Configuration?["LoginEmail"], Password = "WillError"},
            new LoginModel() { LoginEmail = "", Password = Configuration?["Password"] },
            new LoginModel() { LoginEmail = Configuration?["LoginEmail"], Password = ""},
        };

        foreach (LoginModel loginModel in loginModelList)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                string stringData = JsonSerializer.Serialize(loginModel);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/login", contentData).Result;
                Assert.True((int)response.StatusCode == 400);
            }

        }
    }
}

