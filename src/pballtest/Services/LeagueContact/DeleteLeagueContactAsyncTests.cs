namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContactInDB = (from c in db.LeagueContacts
                                                    orderby c.LeagueContactID
                                                    select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(leagueContactInDB);
                if (leagueContactInDB != null)
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

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactInDB.LeagueContactID);
                            LeagueContact? leagueContactRet2 = await DoOKTestReturnLeagueContactAsync(actionDeleteRes);

                            Assert.NotNull(leagueContactRet2);
                            if (leagueContactRet2 != null)
                            {
                                Assert.True(leagueContactRet2.LeagueContactID > 0);
                                Assert.True(leagueContactRet2.Removed);

                                LeagueContact? leagueContactToChange = (from c in db.LeagueContacts
                                                                        where c.LeagueContactID == leagueContactInDB.LeagueContactID
                                                                        select c).FirstOrDefault();

                                Assert.NotNull(leagueContactToChange);
                                if (leagueContactToChange != null)
                                {
                                    try
                                    {
                                        leagueContactToChange.Removed = false;
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
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContactInDB = (from c in db.LeagueContacts
                                                    orderby c.LeagueContactID
                                                    select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(leagueContactInDB);
                if (leagueContactInDB != null)
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
                            UserService.User = null;
                        }

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(1);
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionDeleteRes);
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
    public async Task DeleteLeagueContactAsync_LeagueContactID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContactInDB = (from c in db.LeagueContacts
                                                    orderby c.LeagueContactID
                                                    select c).AsNoTracking().FirstOrDefault();

                Assert.NotNull(leagueContactInDB);
                if (leagueContactInDB != null)
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

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            leagueContactInDB.LeagueContactID = 0;

                            var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactInDB.LeagueContactID);
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueContactID"), actionDeleteRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            leagueContactInDB.LeagueContactID = 10000;

                            actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactInDB.LeagueContactID);
                            boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "LeagueContactID", leagueContactInDB.LeagueContactID.ToString()), actionDeleteRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}

