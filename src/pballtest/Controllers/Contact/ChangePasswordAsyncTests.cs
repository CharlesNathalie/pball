namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await ContactControllerSetup(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
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
            string TempCode = $"{ random.Next(1000, 9999) }";

            if (db != null)
            {
                Contact? contactToAddTempCode = (from c in db.Contacts
                                                 where c.ContactID == contact.ContactID
                                                 select c).FirstOrDefault();

                if (contactToAddTempCode != null)
                {
                    contactToAddTempCode.ResetPasswordTempCode = TempCode;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Assert.True(false, ex.Message);
                }
            }

            if (Configuration != null)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                    {
                        LoginEmail = registerModel.LoginEmail,
                        Password = $"{ registerModel.Password }New",
                        TempCode = TempCode,
                    };

                    string stringData = JsonSerializer.Serialize(changePasswordModel);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/changepassword", contentData).Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);
                }

                loginModel = new LoginModel()
                {
                    LoginEmail = registerModel.LoginEmail,
                    Password = $"{ registerModel.Password }New",
                };

                contact = await DoOkLogin(loginModel, culture);
                Assert.NotNull(contact);
                if (contact != null)
                {
                    Assert.True(contact.ContactID > 0);
                    Assert.NotEmpty(contact.Token);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Error_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await ContactControllerSetup(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
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
            string TempCode = $"{ random.Next(1000, 9999) }";

            if (db != null)
            {
                Contact? contactToAddTempCode = (from c in db.Contacts
                                                 where c.ContactID == contact.ContactID
                                                 select c).FirstOrDefault();

                if (contactToAddTempCode != null)
                {
                    contactToAddTempCode.ResetPasswordTempCode = TempCode;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Assert.True(false, ex.Message);
                }
            }

            if (Configuration != null)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                    {
                        LoginEmail = registerModel.LoginEmail,
                        Password = $"{ registerModel.Password }New",
                        TempCode = "121a", // will create an error
                    };

                    string stringData = JsonSerializer.Serialize(changePasswordModel);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/changepassword", contentData).Result;
                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                    Assert.NotNull(errRes);
                }
            }
        }
    }
}

