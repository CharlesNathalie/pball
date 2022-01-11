namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Good_Test(string culture)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        LeagueContact? leagueContact = new LeagueContact()
                        {
                            LeagueContactID = 0,
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = leagueContactInDB.LeagueID,
                            Removed = false,
                            LastUpdateContactID = leagueContactInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueContactInDB.LastUpdateDate_UTC,
                        };

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                            LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
                            
                            Assert.NotNull(leagueContactRet);
                            if (leagueContactRet != null)
                            {
                                Assert.True(leagueContactRet.LeagueContactID > 0);

                                LeagueContact? leagueContactToDelete = (from c in db.LeagueContacts
                                                                        where c.LeagueContactID == leagueContactRet.LeagueContactID
                                                                        select c).FirstOrDefault();

                                Assert.NotNull(leagueContactToDelete);
                                if (leagueContactToDelete != null)
                                {
                                    try
                                    {
                                        db.LeagueContacts.Remove(leagueContactToDelete);
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
    public async Task AddLeagueContactAsync_Authorization_Error_Test(string culture)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = null;
                        }

                        LeagueContact? leagueContact = new LeagueContact()
                        {
                            LeagueContactID = 0,
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = leagueContactInDB.LeagueID,
                            Removed = false,
                            LastUpdateContactID = leagueContactInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueContactInDB.LastUpdateDate_UTC,
                        };

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            var actionRes = await LeagueContactService.AddLeagueContactAsync(new LeagueContact());
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
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
    public async Task AddLeagueContactAsync_LeagueContactID_Error_Test(string culture)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        LeagueContact? leagueContact = new LeagueContact()
                        {
                            LeagueContactID = 0,
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = leagueContactInDB.LeagueID,
                            Removed = false,
                            LastUpdateContactID = leagueContactInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueContactInDB.LastUpdateDate_UTC,
                        };

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            leagueContact.LeagueContactID = 1;

                            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._ShouldBeEqualTo_, "LeagueContactID", "0"), actionRes);
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
    public async Task AddLeagueContactAsync_LeagueID_Error_Test(string culture)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        LeagueContact? leagueContact = new LeagueContact()
                        {
                            LeagueContactID = 0,
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = leagueContactInDB.LeagueID,
                            Removed = false,
                            LastUpdateContactID = leagueContactInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueContactInDB.LastUpdateDate_UTC,
                        };

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            leagueContact.LeagueID = 0;

                            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            leagueContact.LeagueID = 10000;

                            actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                            boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", leagueContact.LeagueID.ToString()), actionRes);
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
    public async Task AddLeagueContactAsync_ContactID_Error_Test(string culture)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        LeagueContact? leagueContact = new LeagueContact()
                        {
                            LeagueContactID = 0,
                            ContactID = contact.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = leagueContactInDB.LeagueID,
                            Removed = false,
                            LastUpdateContactID = leagueContactInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueContactInDB.LastUpdateDate_UTC,
                        };

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            leagueContact.ContactID = 0;

                            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "ContactID"), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            leagueContact.ContactID = 10000;

                            actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                            boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContact.ContactID.ToString()), actionRes);
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
    public async Task AddLeagueContactAsync_AlreadyExist_Error_Test(string culture)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        LeagueContact? leagueContact = new LeagueContact()
                        {
                            LeagueContactID = 0,
                            ContactID = leagueContactInDB.ContactID,
                            IsLeagueAdmin = true,
                            LeagueID = leagueContactInDB.LeagueID,
                            Removed = false,
                            LastUpdateContactID = leagueContactInDB.LastUpdateContactID,
                            LastUpdateDate_UTC = leagueContactInDB.LastUpdateDate_UTC,
                        };

                        Assert.NotNull(LeagueContactService);
                        if (LeagueContactService != null)
                        {
                            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._AlreadyExist, "LeagueContact"), actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}

