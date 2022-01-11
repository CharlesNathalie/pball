namespace pball.Controllers.Tests;

public partial class LeagueContactControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

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

        if (contact != null)
        {
            League? league = await FillLeagueAsync();

            league = await DoOkLeague(league, contact, culture);
            Assert.NotNull(league);
            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }

            if (league != null)
            {
                if (contact != null)
                {
                    LeagueContact? leagueContact = new LeagueContact()
                    {
                        ContactID = contact.ContactID,
                        IsLeagueAdmin = true,
                        LeagueID = league.LeagueID,
                    };

                    leagueContact = await DoOkLeagueContact(leagueContact, contact, culture);
                    Assert.NotNull(leagueContact);
                    if (leagueContact != null)
                    {
                        Assert.True(leagueContact.LeagueContactID > 0);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Error_Test(string culture)
    {
        Assert.True(await LeagueContactControllerSetup(culture));

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

        if (contact != null)
        {
            League? league = await FillLeagueAsync();

            league = await DoOkLeague(league, contact, culture);
            Assert.NotNull(league);
            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }

            if (league != null)
            {
                if (contact != null)
                {
                    LeagueContact? leagueContact = new LeagueContact()
                    {
                        ContactID = 0, // contact.ContactID, will create an error
                        IsLeagueAdmin = true,
                        LeagueID = league.LeagueID,
                    };

                    if (Configuration != null)
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                            string stringData = JsonSerializer.Serialize(leagueContact);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/leaguecontact", contentData).Result;
                            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                            Assert.NotNull(errRes);
                        }
                    }
                }
            }
        }
    }
}

