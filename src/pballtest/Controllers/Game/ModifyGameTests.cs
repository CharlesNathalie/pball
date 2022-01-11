namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyGameAsync_Good_Test(string culture)
    {
        Random random = new Random();

        Assert.True(await GameControllerSetup(culture));

        bool? boolRet = await ClearServerLoggedInListAsync(culture);
        Assert.True(boolRet);

        RegisterModel registerModel = await FillRegisterModelAsync();

        Contact? contact = await DoOkRegister(registerModel, culture);
        Assert.NotNull(contact);
        if (contact != null)
        {
            Assert.True(contact.ContactID > 0);
        }

        RegisterModel registerModel2 = await FillRegisterModelAsync();

        registerModel2.LoginEmail = "P" + registerModel.LoginEmail;
        registerModel2.FirstName = "P" + registerModel.FirstName;
        registerModel2.LastName = "P" + registerModel.LastName;
        registerModel2.Initial = "P" + registerModel.Initial;

        Contact? contact2 = await DoOkRegister(registerModel2, culture);
        Assert.NotNull(contact2);
        if (contact2 != null)
        {
            Assert.True(contact2.ContactID > 0);
        }

        RegisterModel registerModel3 = await FillRegisterModelAsync();

        registerModel3.LoginEmail = "w" + registerModel.LoginEmail;
        registerModel3.FirstName = "w" + registerModel.FirstName;
        registerModel3.LastName = "w" + registerModel.LastName;
        registerModel3.Initial = "w" + registerModel.Initial;

        Contact? contact3 = await DoOkRegister(registerModel3, culture);
        Assert.NotNull(contact3);
        if (contact3 != null)
        {
            Assert.True(contact3.ContactID > 0);
        }

        RegisterModel registerModel4 = await FillRegisterModelAsync();

        registerModel4.LoginEmail = "r" + registerModel.LoginEmail;
        registerModel4.FirstName = "r" + registerModel.FirstName;
        registerModel4.LastName = "r" + registerModel.LastName;
        registerModel4.Initial = "r" + registerModel.Initial;

        Contact? contact4 = await DoOkRegister(registerModel4, culture);
        Assert.NotNull(contact4);
        if (contact4 != null)
        {
            Assert.True(contact4.ContactID > 0);
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

        if (contact != null && contact2 != null && contact3 != null && contact4 != null)
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
                if (contact != null && contact2 != null && contact3 != null && contact4 != null && league != null)
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

                    LeagueContact? leagueContact2 = new LeagueContact()
                    {
                        ContactID = contact2.ContactID,
                        IsLeagueAdmin = false,
                        LeagueID = league.LeagueID,
                    };

                    leagueContact2 = await DoOkLeagueContact(leagueContact2, contact, culture);
                    Assert.NotNull(leagueContact2);
                    if (leagueContact2 != null)
                    {
                        Assert.True(leagueContact2.LeagueContactID > 0);
                    }

                    LeagueContact? leagueContact3 = new LeagueContact()
                    {
                        ContactID = contact3.ContactID,
                        IsLeagueAdmin = false,
                        LeagueID = league.LeagueID,
                    };

                    leagueContact3 = await DoOkLeagueContact(leagueContact3, contact, culture);
                    Assert.NotNull(leagueContact3);
                    if (leagueContact3 != null)
                    {
                        Assert.True(leagueContact3.LeagueContactID > 0);
                    }

                    LeagueContact? leagueContact4 = new LeagueContact()
                    {
                        ContactID = contact4.ContactID,
                        IsLeagueAdmin = false,
                        LeagueID = league.LeagueID,
                    };

                    leagueContact4 = await DoOkLeagueContact(leagueContact4, contact, culture);
                    Assert.NotNull(leagueContact4);
                    if (leagueContact4 != null)
                    {
                        Assert.True(leagueContact4.LeagueContactID > 0);
                    }

                    Game? game = new Game()
                    {
                        GameDate = DateTime.Now,
                        LastUpdateContactID = contact.ContactID,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = league.LeagueID,
                        Team1Player1 = contact.ContactID,
                        Team1Player2 = contact2.ContactID,
                        Team2Player1 = contact3.ContactID,
                        Team2Player2 = contact4.ContactID,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    game = await DoOkGame(game, contact, culture);
                    Assert.NotNull(game);
                    if (game != null)
                    {
                        Assert.True(game.GameID > 0);
                    }

                    if (game != null)
                    {
                        game.Team1Scores = random.Next(1, 11);
                        game.Team2Scores = random.Next(1, 11);

                        using (HttpClient httpClient = new HttpClient())
                        {
                            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                            if (Configuration != null)
                            {
                                string stringData = JsonSerializer.Serialize(game);
                                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
                                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                                string responseContent = await response.Content.ReadAsStringAsync();
                                Game? gameRet = JsonSerializer.Deserialize<Game>(responseContent);
                                Assert.NotNull(gameRet);
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
        Random random = new Random();

        Assert.True(await GameControllerSetup(culture));

        bool? boolRet = await ClearServerLoggedInListAsync(culture);
        Assert.True(boolRet);

        RegisterModel registerModel = await FillRegisterModelAsync();

        Contact? contact = await DoOkRegister(registerModel, culture);
        Assert.NotNull(contact);
        if (contact != null)
        {
            Assert.True(contact.ContactID > 0);
        }

        RegisterModel registerModel2 = await FillRegisterModelAsync();

        registerModel2.LoginEmail = "P" + registerModel.LoginEmail;
        registerModel2.FirstName = "P" + registerModel.FirstName;
        registerModel2.LastName = "P" + registerModel.LastName;
        registerModel2.Initial = "P" + registerModel.Initial;

        Contact? contact2 = await DoOkRegister(registerModel2, culture);
        Assert.NotNull(contact2);
        if (contact2 != null)
        {
            Assert.True(contact2.ContactID > 0);
        }

        RegisterModel registerModel3 = await FillRegisterModelAsync();

        registerModel3.LoginEmail = "w" + registerModel.LoginEmail;
        registerModel3.FirstName = "w" + registerModel.FirstName;
        registerModel3.LastName = "w" + registerModel.LastName;
        registerModel3.Initial = "w" + registerModel.Initial;

        Contact? contact3 = await DoOkRegister(registerModel3, culture);
        Assert.NotNull(contact3);
        if (contact3 != null)
        {
            Assert.True(contact3.ContactID > 0);
        }

        RegisterModel registerModel4 = await FillRegisterModelAsync();

        registerModel4.LoginEmail = "r" + registerModel.LoginEmail;
        registerModel4.FirstName = "r" + registerModel.FirstName;
        registerModel4.LastName = "r" + registerModel.LastName;
        registerModel4.Initial = "r" + registerModel.Initial;

        Contact? contact4 = await DoOkRegister(registerModel4, culture);
        Assert.NotNull(contact4);
        if (contact4 != null)
        {
            Assert.True(contact4.ContactID > 0);
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

        if (contact != null && contact2 != null && contact3 != null && contact4 != null)
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
                if (contact != null && contact2 != null && contact3 != null && contact4 != null && league != null)
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

                    LeagueContact? leagueContact2 = new LeagueContact()
                    {
                        ContactID = contact2.ContactID,
                        IsLeagueAdmin = false,
                        LeagueID = league.LeagueID,
                    };

                    leagueContact2 = await DoOkLeagueContact(leagueContact2, contact, culture);
                    Assert.NotNull(leagueContact2);
                    if (leagueContact2 != null)
                    {
                        Assert.True(leagueContact2.LeagueContactID > 0);
                    }

                    LeagueContact? leagueContact3 = new LeagueContact()
                    {
                        ContactID = contact3.ContactID,
                        IsLeagueAdmin = false,
                        LeagueID = league.LeagueID,
                    };

                    leagueContact3 = await DoOkLeagueContact(leagueContact3, contact, culture);
                    Assert.NotNull(leagueContact3);
                    if (leagueContact3 != null)
                    {
                        Assert.True(leagueContact3.LeagueContactID > 0);
                    }

                    LeagueContact? leagueContact4 = new LeagueContact()
                    {
                        ContactID = contact4.ContactID,
                        IsLeagueAdmin = false,
                        LeagueID = league.LeagueID,
                    };

                    leagueContact4 = await DoOkLeagueContact(leagueContact4, contact, culture);
                    Assert.NotNull(leagueContact4);
                    if (leagueContact4 != null)
                    {
                        Assert.True(leagueContact4.LeagueContactID > 0);
                    }

                    Game? game = new Game()
                    {
                        GameDate = DateTime.Now,
                        LastUpdateContactID = contact.ContactID,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = league.LeagueID,
                        Team1Player1 = contact.ContactID,
                        Team1Player2 = contact2.ContactID,
                        Team2Player1 = contact3.ContactID,
                        Team2Player2 = contact4.ContactID,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    game = await DoOkGame(game, contact, culture);
                    Assert.NotNull(game);
                    if (game != null)
                    {
                        Assert.True(game.GameID > 0);
                    }

                    if (game != null)
                    {
                        game.Team1Scores = random.Next(1, 11);
                        game.Team2Scores = random.Next(1, 11);

                        game.GameID = game.GameID + 100000;

                        using (HttpClient httpClient = new HttpClient())
                        {
                            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contact.Token);

                            if (Configuration != null)
                            {
                                string stringData = JsonSerializer.Serialize(game);
                                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = httpClient.PutAsync($"{ Configuration["pballurl"] }api/{ culture }/game", contentData).Result;
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
}

