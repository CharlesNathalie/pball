namespace pball.Controllers.Tests;

public partial class LeagueControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await LeagueControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? league = (from c in db.Leagues
                                  orderby c.LeagueID
                                  select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(league);
                if (league != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
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

                                        response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/league/{ league.LeagueID }").Result;
                                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                        responseContent = await response.Content.ReadAsStringAsync();
                                        League? leagueRet = JsonSerializer.Deserialize<League>(responseContent);
                                        Assert.NotNull(leagueRet);
                                        if (leagueRet != null)
                                        {
                                            Assert.True(leagueRet.LeagueID > 0);

                                            League? leagueToChange = (from c in db.Leagues
                                                                      where c.LeagueID == leagueRet.LeagueID
                                                                      select c).FirstOrDefault();

                                            Assert.NotNull(leagueToChange);
                                            if (leagueToChange != null)
                                            {
                                                try
                                                {
                                                    leagueToChange.Removed = false;
                                                    db.SaveChanges();
                                                }
                                                catch (Exception ex)
                                                {
                                                    Assert.True(false, ex.Message);
                                                }
                                            }
                                        }
                                    }
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
    public async Task DeleteLeagueAsync_Error_Test(string culture)
    {
        Assert.True(await LeagueControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Leagues);
            if (db.Leagues != null)
            {
                League? league = (from c in db.Leagues
                                  orderby c.LeagueID
                                  select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(league);
                if (league != null)
                {
                    Assert.NotNull(db.Contacts);
                    if (db.Contacts != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
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
                                        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                        response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/league/{ league.LeagueID }").Result;
                                        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                                        responseContent = await response.Content.ReadAsStringAsync();
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
        }
    }
}

