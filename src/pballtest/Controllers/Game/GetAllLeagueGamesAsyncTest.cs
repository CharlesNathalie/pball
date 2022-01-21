namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllLeagueGamesAsync_Good_Test(string culture)
    {
        Assert.True(await GameControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Contact? contact = (from c in db.Contacts
                                select c).AsNoTracking().FirstOrDefault();

            if (contact != null)
            {
                Assert.NotNull(db.Games);
                if (db.Games != null)
                {
                    Game? game = (from c in db.Games
                                  select c).AsNoTracking().FirstOrDefault();

                    Assert.NotNull(game);
                    if (game != null)
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

                                    response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/game/getallleaguegames/{game.LeagueID}").Result;
                                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                    responseContent = await response.Content.ReadAsStringAsync();
                                    List<Game>? gameList = JsonSerializer.Deserialize<List<Game>>(responseContent);
                                    Assert.NotNull(gameList);
                                    if (gameList != null)
                                    {
                                        Assert.NotEmpty(gameList);
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
    public async Task GetAllLeagueGamesAsync_Error_Test(string culture)
    {
        Assert.True(await GameControllerSetup(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Contact? contact = (from c in db.Contacts
                                select c).AsNoTracking().FirstOrDefault();

            if (contact != null)
            {
                Assert.NotNull(db.Games);
                if (db.Games != null)
                {
                    Game? game = (from c in db.Games
                                  select c).AsNoTracking().FirstOrDefault();

                    Assert.NotNull(game);
                    if (game != null)
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


                                    response = httpClient.GetAsync($"{ Configuration["pballurl"] }api/{ culture }/game/getallleaguegames/{-1}").Result;
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

