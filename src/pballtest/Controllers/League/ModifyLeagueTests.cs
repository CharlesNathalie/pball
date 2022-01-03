namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await LeagueControllerSetup(culture));

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
            League? league = await FillLeagueAsync();

            league = await DoOkLeague(league, contact, culture);
            Assert.NotNull(league);
            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }

            if (league != null)
            {
                league.LeagueName = league.LeagueName + "new";

                if (Configuration != null)
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                        string stringData = JsonSerializer.Serialize(league);
                        var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/league", contentData).Result;
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                        string responseContent = await response.Content.ReadAsStringAsync();
                        league = JsonSerializer.Deserialize<League>(responseContent);
                        Assert.NotNull(league);
                        if (league != null)
                        {
                            Assert.True(league.LeagueID > 0);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueAsync_Error_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await LeagueControllerSetup(culture));

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
            League? league = await FillLeagueAsync();

            league = await DoOkLeague(league, contact, culture);
            Assert.NotNull(league);
            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }

            if (league != null)
            {
                league.LeagueName = "";

                if (Configuration != null)
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                        string stringData = JsonSerializer.Serialize(league);
                        var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/league", contentData).Result;
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

