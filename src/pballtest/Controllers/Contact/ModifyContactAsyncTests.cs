namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        boolRet = await ClearServerLoggedInListAsync(culture);
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

        if (contact != null)
        {
            contact.FirstName = "new" + contact.FirstName;

            if (Configuration != null)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                    string stringData = JsonSerializer.Serialize(contact);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/contact", contentData).Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Contact? contactRet = JsonSerializer.Deserialize<Contact>(responseContent);
                    Assert.NotNull(contactRet);
                    if (contactRet != null)
                    {
                        Assert.True(contactRet.ContactID > 0);
                        Assert.Equal(contact.FirstName, contactRet.FirstName);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Error_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        boolRet = await ClearServerLoggedInListAsync(culture);
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

        loginModel.LoginEmail = "";

        ErrRes? errRes = await DoBadRequestLogin(loginModel, culture);
        Assert.NotNull(errRes);
        if (errRes != null)
        {
            Assert.NotEmpty(errRes.ErrList);
        }
    }
}

