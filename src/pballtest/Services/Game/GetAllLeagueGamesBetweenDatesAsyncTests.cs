namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllLeagueGamesBetweenDatesAsync_Good_Test(string culture)
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

                        if (gameInDB != null)
                        {
                            LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                            {
                                LeagueID = gameInDB.LeagueID,
                                StartDate = DateTime.Now.AddDays(-10),
                                EndDate = DateTime.Now.AddDays(10),
                            };

                            if (GameService != null)
                            {
                                var actionGetAllRes = await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
                                List<Game>? gameList = await DoOKTestReturnGameListAsync(actionGetAllRes);
                                Assert.NotNull(gameList);
                                Assert.NotEmpty(gameList);

                                leagueGamesModel.StartDate = DateTime.Now.AddDays(+100);
                                leagueGamesModel.EndDate = DateTime.Now.AddDays(+101);

                                actionGetAllRes = await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
                                gameList = await DoOKTestReturnGameListAsync(actionGetAllRes);
                                Assert.NotNull(gameList);
                                Assert.Empty(gameList);
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
    public async Task GetAllLeagueGamesBetweenDatesAsync_Authorization_Error_Test(string culture)
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

                        if (gameInDB != null)
                        {
                            LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                            {
                                LeagueID = gameInDB.LeagueID,
                                StartDate = DateTime.Now.AddDays(-10),
                                EndDate = DateTime.Now.AddDays(10),
                            };

                            if (GameService != null)
                            {
                                var actionGetAllRes = await GameService.GetAllLeagueGamesBetweenDatesAsync(new LeagueGamesModel());
                                bool? boolRet = await DoBadRequestGameListTestAsync(PBallRes.YouDoNotHaveAuthorization, actionGetAllRes);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);
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
    public async Task GetAllLeagueGamesBetweenDatesAsync_LeagueID_Error_Test(string culture)
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

                        if (gameInDB != null)
                        {
                            LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                            {
                                LeagueID = 0,
                                StartDate = DateTime.Now.AddDays(-1),
                                EndDate = DateTime.Now.AddDays(1),
                            };

                            if (GameService != null)
                            {
                                var actionGetAllRes = await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
                                bool? boolRet = await DoBadRequestGameListTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionGetAllRes);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);
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
    public async Task GetAllLeagueGamesBetweenDatesAsync_StartDate_Error_Test(string culture)
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

                        if (gameInDB != null)
                        {
                            if (GameService != null)
                            {
                                LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                                {
                                    LeagueID = 1,
                                    StartDate = new DateTime(),
                                    EndDate = DateTime.Now.AddDays(1),
                                };

                                var actionGetAllRes = await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
                                bool? boolRet = await DoBadRequestGameListTestAsync(string.Format(PBallRes._IsRequired, "StartDate"), actionGetAllRes);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);
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
    public async Task GetAllLeagueGamesBetweenDatesAsync_EndDate_Error_Test(string culture)
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

                        if (gameInDB != null)
                        {
                            if (GameService != null)
                            {
                                LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                                {
                                    LeagueID = 1,
                                    StartDate = DateTime.Now.AddDays(-1),
                                    EndDate = new DateTime(),
                                };

                                var actionGetAllRes = await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
                                bool? boolRet = await DoBadRequestGameListTestAsync(string.Format(PBallRes._IsRequired, "EndDate"), actionGetAllRes);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);
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
    public async Task GetAllLeagueGamesBetweenDatesAsync_StartDate_Bigger_EndDate_Error_Test(string culture)
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

                        if (gameInDB != null)
                        {
                            if (GameService != null)
                            {
                                LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                                {
                                    LeagueID = 1,
                                    StartDate = DateTime.Now.AddDays(-1),
                                    EndDate = DateTime.Now.AddDays(-2),
                                };

                                var actionGetAllRes = await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
                                bool? boolRet = await DoBadRequestGameListTestAsync(string.Format(PBallRes._DateIsBiggerThan_, "StartDate", "EndDate"), actionGetAllRes);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);
                            }
                        }
                    }
                }
            }
        }
    }
}

