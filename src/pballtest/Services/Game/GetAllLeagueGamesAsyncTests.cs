namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetAllLeagueGamesAsync_Good_Test(string culture)
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
                                var actionGetAllRes = await GameService.GetAllLeagueGamesAsync(gameInDB.LeagueID);
                                List<Game>? gameList = await DoOKTestReturnGameListAsync(actionGetAllRes);
                                Assert.NotNull(gameList);
                                Assert.NotEmpty(gameList);
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
    public async Task GetAllLeagueGamesAsync_Authorization_Error_Test(string culture)
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
                            if (GameService != null)
                            {
                                var actionGetAllRes = await GameService.GetAllLeagueGamesAsync(-1);
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
    public async Task GetAllLeagueGamesAsync_LeagueID_Error_Test(string culture)
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
                                var actionGetAllRes = await GameService.GetAllLeagueGamesAsync(-1);
                                bool? boolRet = await DoBadRequestGameListTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", "-1"), actionGetAllRes);
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

