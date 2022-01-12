namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyGameAsync_Good_Test(string culture)
    {
        Assert.True(await GameControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
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
                                        game.Team1Scores = game.Team1Scores + 1;

                                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                        stringData = JsonSerializer.Serialize(game);
                                        contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                        response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
                                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                        responseContent = await response.Content.ReadAsStringAsync();
                                        Game? gameRet = JsonSerializer.Deserialize<Game>(responseContent);
                                        Assert.NotNull(gameRet);
                                        if (gameRet != null)
                                        {
                                            Assert.True(gameRet.LeagueID > 0);
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
    public async Task ModifyGameAsync_Error_Test(string culture)
    {
        Assert.True(await GameControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
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
                                        game.Team1Scores = -1;

                                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                        stringData = JsonSerializer.Serialize(game);
                                        contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                        response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
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

