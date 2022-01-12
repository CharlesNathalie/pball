namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteGameAsync_Good_Test(string culture)
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
                                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                        response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/game/{ game.GameID }").Result;
                                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                        responseContent = await response.Content.ReadAsStringAsync();
                                        Game? gameRet = JsonSerializer.Deserialize<Game>(responseContent);

                                        Assert.NotNull(gameRet);
                                        if (gameRet != null)
                                        {
                                            Assert.True(game.GameID > 0);

                                            Game? GameToChange = (from c in db.Games
                                                                  where c.GameID == game.GameID
                                                                  select c).FirstOrDefault();

                                            Assert.NotNull(GameToChange);
                                            if (GameToChange != null)
                                            {
                                                try
                                                {
                                                    GameToChange.Removed = false;
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
    public async Task DeleteGameAsync_Error_Test(string culture)
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
                                        game.GameID = -1;

                                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contactLogin.Token);

                                        response = httpClient.DeleteAsync($"{ Configuration["pballurl"] }api/{ culture }/game/{ game.GameID }").Result;
                                        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                                        responseContent = await response.Content.ReadAsStringAsync();
                                        ErrRes? errRes = JsonSerializer.Deserialize<ErrRes>(responseContent);
                                        Assert.NotNull(errRes);
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

