namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLeagueListAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    if (Configuration != null)
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            LoginModel loginModel = new LoginModel()
                            {
                                LoginEmail = contact.LoginEmail,
                                Password = contact.LastName,
                            };

                            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                            string stringData = JsonSerializer.Serialize(loginModel);
                            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = httpClient.PostAsync($"{ Configuration["pballurl"] }api/{ culture }/contact/login", contentData).Result;
                            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            Contact? contactLogin = JsonSerializer.Deserialize<Contact>(responseContent);

                            Assert.NotNull(contactLogin);
                            if (contactLogin != null)
                            {
                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/league/getleaguelist/0/2").Result;
                                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                responseContent = await response.Content.ReadAsStringAsync();
                                List<League>? leagueListRet = JsonSerializer.Deserialize<List<League>>(responseContent);
                                Assert.NotNull(leagueListRet);
                                if (leagueListRet != null)
                                {
                                    Assert.NotEmpty(leagueListRet);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLeagueListAsync_Error_Test(string culture)
    {
        Assert.True(await LeagueControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    if (Configuration != null)
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            HttpResponseMessage response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/league/getleaguelist/0/2").Result;
                            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                            string responseContent = await response.Content.ReadAsStringAsync();
                            ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                            Assert.NotNull(errRes);
                            if (errRes != null)
                            {
                                Assert.NotEmpty(errRes.ErrList);
                            }
                        }
                    }
                }
            }
        }
    }
}

