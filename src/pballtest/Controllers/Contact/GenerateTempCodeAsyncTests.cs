namespace pball.Controllers.Tests;

public partial class ContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_Good_Test(string culture)
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

        RegisterModel registerModel2 = await FillRegisterModelAsync();

        Contact? contact2 = await DoOkRegister(registerModel2, culture);
        Assert.NotNull(contact2);
        if (contact2 != null)
        {
            Assert.True(contact2.ContactID > 0);
        }

        League? league = await FillLeagueAsync();

        if (contact != null)
        {
            league = await DoOkLeague(league, contact, culture);
            Assert.NotNull(league);
            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }
        }

        LeagueContact? leagueContact = null;
        LeagueContact? leagueContact2 = null;

        if (league != null && contact != null && contact2 != null)
        {
            leagueContact = await FillLeagueContactAsync(league.LeagueID, contact.ContactID, true);

            if (leagueContact != null)
            {
                leagueContact = await DoOkLeagueContact(leagueContact, contact, culture);
                Assert.NotNull(leagueContact);
                if (leagueContact != null)
                {
                    Assert.True(leagueContact.LeagueContactID > 0);
                }
            }

            leagueContact2 = await FillLeagueContactAsync(league.LeagueID, contact2.ContactID, true);

            if (leagueContact != null)
            {
                leagueContact2 = await DoOkLeagueContact(leagueContact2, contact, culture);
                Assert.NotNull(leagueContact2);
                if (leagueContact2 != null)
                {
                    Assert.True(leagueContact2.LeagueContactID > 0);
                }
            }
        }

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                if (contact != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);
                }

                if (contact != null && contact2 != null && league != null)
                {

                    LoginEmailModel loginEmailModel = new LoginEmailModel()
                    {
                        LoginEmail = contact2.LoginEmail,
                    };

                    string stringData = JsonSerializer.Serialize(loginEmailModel);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/generatetempcode", contentData).Result;
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    boolRet = JsonSerializer.Deserialize<bool>(responseContent);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_Error_Test(string culture)
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

        RegisterModel registerModel2 = await FillRegisterModelAsync();

        Contact? contact2 = await DoOkRegister(registerModel2, culture);
        Assert.NotNull(contact2);
        if (contact2 != null)
        {
            Assert.True(contact2.ContactID > 0);
        }

        League? league = await FillLeagueAsync();

        if (contact != null)
        {
            league = await DoOkLeague(league, contact, culture);
            Assert.NotNull(league);
            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }
        }

        LeagueContact? leagueContact = null;
        LeagueContact? leagueContact2 = null;

        if (league != null && contact != null && contact2 != null)
        {
            leagueContact = await FillLeagueContactAsync(league.LeagueID, contact.ContactID, true);

            if (leagueContact != null)
            {
                leagueContact = await DoOkLeagueContact(leagueContact, contact, culture);
                Assert.NotNull(leagueContact);
                if (leagueContact != null)
                {
                    Assert.True(leagueContact.LeagueContactID > 0);
                }
            }

            leagueContact2 = await FillLeagueContactAsync(league.LeagueID, contact2.ContactID, true);

            if (leagueContact != null)
            {
                leagueContact2 = await DoOkLeagueContact(leagueContact2, contact, culture);
                Assert.NotNull(leagueContact2);
                if (leagueContact2 != null)
                {
                    Assert.True(leagueContact2.LeagueContactID > 0);
                }
            }
        }

        if (Configuration != null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                if (contact != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);
                }

                if (contact != null && contact2 != null && league != null)
                {
                    LoginEmailModel loginEmailModel = new LoginEmailModel()
                    {
                        LoginEmail = contact2.LoginEmail,
                    };

                    loginEmailModel.LoginEmail = "";

                    string stringData = JsonSerializer.Serialize(loginEmailModel);
                    var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/generatetempcode", contentData).Result;
                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                    string responseContent = await response.Content.ReadAsStringAsync();
                    ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                    Assert.NotNull(errRes);
                }
            }
        }
    }
}

