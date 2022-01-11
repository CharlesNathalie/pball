namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLoginEmailExistAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        bool? boolRet = await ClearServerLoggedInListAsync(culture);
        Assert.True(boolRet);

        RegisterModel registerModel = await FillRegisterModelAsync();

        Contact? contact = await DoOkRegister(registerModel, culture);
        Assert.NotNull(contact);
        if (contact != null)
        {
            Assert.True(contact.ContactID > 0);
        }

        LoginModel loginModel = new LoginModel()
        {
            LoginEmail = registerModel.LoginEmail,
            Password = registerModel.Password,
        };

        contact = await DoOkLogin(loginModel, culture);
        Assert.NotNull(contact);
        if (contact != null)
        {
            Assert.True(contact.ContactID > 0);
            Assert.NotEmpty(contact.Token);
        }

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                LoginEmailModel loginEmailModel = new LoginEmailModel()
                {
                    LoginEmail = registerModel.LoginEmail,
                };

                string stringData = JsonSerializer.Serialize(loginEmailModel);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getloginemailexist", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                Assert.True(boolRet);

                loginEmailModel.LoginEmail = $"Not{ registerModel.LoginEmail}";

                stringData = JsonSerializer.Serialize(loginEmailModel);
                contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/getloginemailexist", contentData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                responseContent = await response.Content.ReadAsStringAsync();
                boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                Assert.False(boolRet);
            }
        }
    }
}

