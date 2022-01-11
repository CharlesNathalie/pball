namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await LeagueControllerSetup(culture));

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

            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                if (league != null)
                {
                    if (Configuration != null)
                    {
                        HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/league/{ league.LeagueID }").Result;
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
    public async Task DeleteLeagueAsync_Error_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await LeagueControllerSetup(culture));

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

            using (HttpClient httpClient = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                if (league != null)
                {
                    if (Configuration != null)
                    {
                        HttpResponseMessage response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/league/{ league.LeagueID + 10000 }").Result;
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

