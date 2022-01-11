namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Good_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).AsNoTracking().FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            var actionRes = await GameService.AddGameAsync(game);
                            Game? gameRet = await DoOKTestReturnGameAsync(actionRes);
                            Assert.NotNull(gameRet);
                            if (gameRet != null)
                            {
                                Assert.True(gameRet.GameID > 0);

                                Game? gameToDelete = (from c in db.Games
                                                      where c.GameID == gameRet.GameID
                                                      select c).FirstOrDefault();

                                Assert.NotNull(gameToDelete);
                                if (gameToDelete != null)
                                {
                                    if (db.Games != null)
                                    {
                                        try
                                        {
                                            db.Games.Remove(gameToDelete);
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
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));
        
        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = null;
                            }

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_GameIDShouldBe0_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 1,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._ShouldBeEqualTo_, "GameID", "0"), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            game.LeagueID = 0;

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "LeagueID", "0"), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            game.LeagueID = -1;

                            actionRes = await GameService.AddGameAsync(game);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", game.LeagueID.ToString()), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team1Player1_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            game.Team1Player1 = 0;

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player1"), actionRes);
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
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team1Player2_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            game.Team1Player2 = 0;

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player2"), actionRes);
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
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team2Player1_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            game.Team2Player1 = 0;

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player1"), actionRes);
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
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team2Player2_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            game.Team2Player2 = 0;

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player2"), actionRes);
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
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team1Scores_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            game.Team1Scores = -1;

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._MinValueIs_, "Team1Scores", "0"), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddGameAsync_Team2Scores_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);

                if (gameInDB != null)
                {
                    Game game = new Game()
                    {
                        GameID = 0,
                        GameDate = DateTime.Now,
                        LastUpdateContactID = gameInDB.Team1Player2,
                        LastUpdateDate_UTC = DateTime.Now,
                        LeagueID = gameInDB.LeagueID,
                        Team1Player1 = gameInDB.Team1Player1,
                        Team1Player2 = gameInDB.Team1Player2,
                        Team2Player1 = gameInDB.Team2Player1,
                        Team2Player2 = gameInDB.Team2Player2,
                        Team1Scores = 4,
                        Team2Scores = 4,
                        Removed = false,
                    };

                    if (GameService != null)
                    {
                        Contact? contact = (from c in db.Contacts
                                            orderby c.ContactID
                                            select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;
                            }

                            game.Team2Scores = -1;

                            var actionRes = await GameService.AddGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._MinValueIs_, "Team2Scores", "0"), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}

