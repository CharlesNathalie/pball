namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LogoffAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        RegisterModel registerModel = await FillRegisterModel();

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

        if (contact != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/logoff/{ contact.ContactID }").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                bool? boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LogoffAsync_Error_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        RegisterModel registerModel = await FillRegisterModel();

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

        if (contact != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                contact.ContactID = 0;

                HttpResponseMessage response = httpClient.GetAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/logoff/{ contact.ContactID }").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                string responseContent = await response.Content.ReadAsStringAsync();
                ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                Assert.NotNull(errRes);
                if (errRes != null)
                {
                    Assert.NotEmpty(errRes.ErrList);
                }

                contact.ContactID = 10000;

                response = httpClient.GetAsync($"{ Configuration?["pballurl"] }api/{ culture }/contact/logoff/{ contact.ContactID }").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                responseContent = await response.Content.ReadAsStringAsync();
                errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                Assert.NotNull(errRes);
                if (errRes != null)
                {
                    Assert.NotEmpty(errRes.ErrList);
                }
            }
        }
    }
}

