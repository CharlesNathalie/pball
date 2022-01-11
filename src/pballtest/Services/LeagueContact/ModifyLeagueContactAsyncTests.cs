namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_Good_Test(string culture)
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
                                        from lc in db.LeagueContacts
                                        where c.ContactID != lc.ContactID
                                        && lc.LeagueID == leagueContactInDB.LeagueID
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
                            leagueContactInDB.IsLeagueAdmin = !leagueContactInDB.IsLeagueAdmin;
                            int ContactID = leagueContactInDB.ContactID;
                            leagueContactInDB.ContactID = contact.ContactID;

                            var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(leagueContactInDB);
                            LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionModifyRes);
                            
                            Assert.NotNull(leagueContactRet);
                            if (leagueContactRet != null)
                            {
                                Assert.Equal(leagueContactInDB.LeagueID, leagueContactRet.LeagueID);
                                Assert.Equal(leagueContactInDB.IsLeagueAdmin, leagueContactRet.IsLeagueAdmin);

                                if (leagueContactRet != null)
                                {
                                    leagueContactInDB.IsLeagueAdmin = !leagueContactRet.IsLeagueAdmin;
                                    leagueContactInDB.ContactID = ContactID;

                                    var actionModifyRes2 = await LeagueContactService.ModifyLeagueContactAsync(leagueContactInDB);
                                    LeagueContact? leagueContactRet2 = await DoOKTestReturnLeagueContactAsync(actionModifyRes);

                                    Assert.NotNull(leagueContactRet2);
                                    if (leagueContactRet2 != null)
                                    {
                                        Assert.Equal(leagueContactInDB.LeagueID, leagueContactRet2.LeagueID);
                                        Assert.Equal(leagueContactInDB.IsLeagueAdmin, leagueContactRet2.IsLeagueAdmin);
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
    public async Task ModifyLeagueContactAsync_Authorization_Error_Test(string culture)
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
                                        from lc in db.LeagueContacts
                                        where c.ContactID != lc.ContactID
                                        && lc.LeagueID == leagueContactInDB.LeagueID
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
                            var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(new LeagueContact());
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionModifyRes);
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
    public async Task ModifyLeagueContactAsync_LeagueContactID_Error_Test(string culture)
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
                                        from lc in db.LeagueContacts
                                        where c.ContactID != lc.ContactID
                                        && lc.LeagueID == leagueContactInDB.LeagueID
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

                            var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(leagueContactInDB);
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueContactID"), actionModifyRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}

