namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteGameAsync_Good_Test(string culture)
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

                        if (GameService != null)
                        {
                            var actionDeleteRes = await GameService.DeleteGameAsync(gameInDB.GameID);
                            Game? gameRet2 = await DoOKTestReturnGameAsync(actionDeleteRes);
                            
                            Assert.NotNull(gameRet2);
                            if (gameRet2 != null)
                            {
                                Assert.True(gameRet2.GameID > 0);
                                Assert.True(gameRet2.Removed);

                                gameInDB.Removed = false;
                                try
                                {
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
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteGameAsync_Authorization_Error_Test(string culture)
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

                        if (GameService != null)
                        {
                            var actionDeleteRes = await GameService.DeleteGameAsync(gameInDB.GameID);
                            bool? boolRet = await DoBadRequestGameTestAsync(PBallRes.YouDoNotHaveAuthorization, actionDeleteRes);
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
    public async Task DeleteGameAsync_GameID_Error_Test(string culture)
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

                        if (GameService != null)
                        {
                            int GameID = 0;

                            var actionDeleteRes = await GameService.DeleteGameAsync(GameID);
                            bool? boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes._IsRequired, "GameID"), actionDeleteRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            GameID = -1;

                            actionDeleteRes = await GameService.DeleteGameAsync(GameID);
                            boolRet = await DoBadRequestGameTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Game", "GameID", GameID.ToString()), actionDeleteRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}

