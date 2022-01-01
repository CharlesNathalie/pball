namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Good_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            RegisterModel registerModel2 = await FillRegisterModelAsync();

            registerModel2.LoginEmail = "P" + registerModel.LoginEmail;
            registerModel2.FirstName = "P" + registerModel.FirstName;
            registerModel2.LastName = "P" + registerModel.LastName;
            registerModel2.Initial = "P" + registerModel.Initial;

            var actionRegisterRes2 = await ContactService.RegisterAsync(registerModel2);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionRegisterRes2);
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

            var actionRegisterRes3 = await ContactService.RegisterAsync(registerModel3);
            Contact? contact3 = await DoOKTestReturnContactAsync(actionRegisterRes3);
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

            var actionRegisterRes4 = await ContactService.RegisterAsync(registerModel4);
            Contact? contact4 = await DoOKTestReturnContactAsync(actionRegisterRes4);
            Assert.NotNull(contact4);

            if (contact4 != null)
            {
                Assert.True(contact4.ContactID > 0);
            }

            if (LeagueService != null)
            {
                League leagueNew = await FillLeague();

                var actionAddLeagueRes = await LeagueService.AddLeagueAsync(leagueNew);
                League? league = await DoOKTestReturnLeagueAsync(actionAddLeagueRes);
                Assert.NotNull(league);

                if (league != null)
                {
                    Assert.True(league.LeagueID > 0);
                }

                if (LeagueContactService != null)
                {
                    if (contact != null && contact2 != null && contact3 != null && contact4 != null && league != null)
                    {
                        LeagueContact leagueContactNew = new LeagueContact()
                        {
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes = await LeagueContactService.AddLeagueContactAsync(leagueContactNew);
                        LeagueContact? leagueContact = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes);
                        Assert.NotNull(leagueContact);

                        if (leagueContact != null)
                        {
                            Assert.True(leagueContact.LeagueContactID > 0);
                        }

                        LeagueContact leagueContactNew2 = new LeagueContact()
                        {
                            ContactID = contact2.ContactID,
                            IsLeagueAdmin = false,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes2 = await LeagueContactService.AddLeagueContactAsync(leagueContactNew2);
                        LeagueContact? leagueContact2 = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes2);
                        Assert.NotNull(leagueContact2);

                        if (leagueContact2 != null)
                        {
                            Assert.True(leagueContact2.LeagueContactID > 0);
                        }

                        LeagueContact leagueContactNew3 = new LeagueContact()
                        {
                            ContactID = contact3.ContactID,
                            IsLeagueAdmin = false,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes3 = await LeagueContactService.AddLeagueContactAsync(leagueContactNew3);
                        LeagueContact? leagueContact3 = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes3);
                        Assert.NotNull(leagueContact3);

                        if (leagueContact3 != null)
                        {
                            Assert.True(leagueContact3.LeagueContactID > 0);
                        }

                        LeagueContact leagueContactNew4 = new LeagueContact()
                        {
                            ContactID = contact4.ContactID,
                            IsLeagueAdmin = false,
                            LeagueID = league.LeagueID,
                        };

                        var actionAddLeagueContactRes4 = await LeagueContactService.AddLeagueContactAsync(leagueContactNew4);
                        LeagueContact? leagueContact4 = await DoOKTestReturnLeagueContactAsync(actionAddLeagueContactRes4);
                        Assert.NotNull(leagueContact4);

                        if (leagueContact4 != null)
                        {
                            Assert.True(leagueContact4.LeagueContactID > 0);
                        }

                        Game game = new Game()
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

                        if (GameService != null)
                        {
                            var actionRes = await GameService.AddGameAsync(game);
                            Game? gameRet = await DoOKTestReturnGameAsync(actionRes);
                            Assert.NotNull(gameRet);
                            if (gameRet != null)
                            {
                                Assert.True(gameRet.GameID > 0);
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
    public async Task AddGameAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (UserService != null)
            {
                UserService.User = null;
            }

            if (GameService != null)
            {
                var actionRes = await GameService.AddGameAsync(new Game());
                boolRet = await DoBadRequestGameTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_GameIDShouldBe0_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = new Game()
                {
                    GameID = 1,
                };

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._ShouldBeEqualTo_, "GameID", "0"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = new Game()
                {
                    GameID = 0,
                    LeagueID = 0,
                };

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "LeagueID", "0"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                game.LeagueID = 10000;

                actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "LeagueID", "0"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team1Player1_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = await FillGame();

                game.Team1Player1 = 0;

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player1"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                game.Team1Player1 = 10000;

                actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player1"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team1Player2_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = await FillGame();

                game.Team1Player2 = 0;

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player2"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                game.Team1Player2 = 10000;

                actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player2"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team2Player1_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = await FillGame();

                game.Team2Player1 = 0;

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player1"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                game.Team2Player1 = 10000;

                actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player1"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team2Player2_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = await FillGame();

                game.Team2Player2 = 0;

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player2"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                game.Team2Player2 = 10000;

                actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player2"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team1Scores_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = await FillGame();

                game.Team1Scores = -1;

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._MinValueIs_, "Team1Scores", "0"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team2Scores_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRegisterRes);
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

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (GameService != null)
            {
                Game game = await FillGame();

                game.Team2Scores = -1;

                var actionRes = await GameService.AddGameAsync(game);
                boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._MinValueIs_, "Team2Scores", "0"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

