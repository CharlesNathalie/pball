namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyGameAsync_Good_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  orderby c.GameID
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);
                if (gameInDB != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                          orderby c.GameID
                                          select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            gameInDB.Team1Player1 = gameInDB2.Team1Player1;
                            gameInDB.Team1Player2 = gameInDB2.Team1Player2;
                            gameInDB.Team2Player1 = gameInDB2.Team2Player1;
                            gameInDB.Team2Player2 = gameInDB2.Team2Player2;
                            gameInDB.Team1Scores = gameInDB2.Team1Scores;
                            gameInDB.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            var actionModifyRes = await GameService.ModifyGameAsync(gameInDB);
                            Game? gameRet2 = await DoOKTestReturnGameAsync(actionModifyRes);
                            Assert.NotNull(gameRet2);
                            if (gameRet2 != null)
                            {
                                Assert.True(gameRet2.GameID > 0);
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
    public async Task ModifyGameAsync_Autorization_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? gameInDB = (from c in db.Games
                                  orderby c.GameID
                                  select c).FirstOrDefault();

                Assert.NotNull(gameInDB);
                if (gameInDB != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = null;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            gameInDB.Team1Player1 = gameInDB2.Team1Player1;
                            gameInDB.Team1Player2 = gameInDB2.Team1Player2;
                            gameInDB.Team2Player1 = gameInDB2.Team2Player1;
                            gameInDB.Team2Player2 = gameInDB2.Team2Player2;
                            gameInDB.Team1Scores = gameInDB2.Team1Scores;
                            gameInDB.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            var actionModifyRes = await GameService.ModifyGameAsync(new Game());
                            bool? boolRet = await DoBadRequestGameTestAsync(PBallRes.YouDoNotHaveAuthorization, actionModifyRes);
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
    public async Task ModifyGameAsync_GameID_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                                  orderby c.GameID
                                  select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.GameID = 0;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "GameID"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            game.GameID = 10000;

                            actionModifyRes = await GameService.ModifyGameAsync(game);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Game", "GameID", game.GameID.ToString()), actionModifyRes);
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
    public async Task ModifyGameAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.LeagueID = 0;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            game.LeagueID = 10000;

                            actionModifyRes = await GameService.ModifyGameAsync(game);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionModifyRes);
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
    public async Task ModifyGameAsync_Team1Player1_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.Team1Player1 = 0;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player1"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            game.Team1Player1 = 10000;

                            actionModifyRes = await GameService.ModifyGameAsync(game);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player1"), actionModifyRes);
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
    public async Task ModifyGameAsync_Team1Player2_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.Team1Player2 = 0;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player2"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            game.Team1Player2 = 10000;

                            actionModifyRes = await GameService.ModifyGameAsync(game);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team1Player2"), actionModifyRes);
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
    public async Task ModifyGameAsync_Team2Player1_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.Team2Player1 = 0;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player1"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            game.Team2Player1 = 10000;

                            actionModifyRes = await GameService.ModifyGameAsync(game);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player1"), actionModifyRes);
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
    public async Task ModifyGameAsync_Team2Player2_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.Team2Player2 = 0;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player2"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            game.Team2Player2 = 10000;

                            actionModifyRes = await GameService.ModifyGameAsync(game);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "Team2Player2"), actionModifyRes);
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
    public async Task ModifyGameAsync_Team1Scores_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.Team1Scores = -1;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._MinValueIs_, "Team1Scores", "0"), actionModifyRes);
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
    public async Task ModifyGameAsync_Team2Scores_Error_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Games);
            if (db.Games != null)
            {
                Game? game = (from c in db.Games
                              orderby c.GameID
                              select c).FirstOrDefault();

                Assert.NotNull(game);
                if (game != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(db.Contacts);
                        if (db.Contacts != null)
                        {
                            Contact? contactInDB = (from c in db.Contacts
                                                    orderby c.ContactID
                                                    select c).FirstOrDefault();

                            Assert.NotNull(contactInDB);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contactInDB;
                            }
                        }

                        Game? gameInDB2 = (from c in db.Games
                                           orderby c.GameID
                                           select c).Skip(1).FirstOrDefault();

                        Assert.NotNull(gameInDB2);
                        if (gameInDB2 != null)
                        {
                            game.Team1Player1 = gameInDB2.Team1Player1;
                            game.Team1Player2 = gameInDB2.Team1Player2;
                            game.Team2Player1 = gameInDB2.Team2Player1;
                            game.Team2Player2 = gameInDB2.Team2Player2;
                            game.Team1Scores = gameInDB2.Team1Scores;
                            game.Team2Scores = gameInDB2.Team2Scores;
                        }

                        Assert.NotNull(GameService);
                        if (GameService != null)
                        {
                            game.Team2Scores = -1;

                            var actionModifyRes = await GameService.ModifyGameAsync(game);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._MinValueIs_, "Team2Scores", "0"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}

