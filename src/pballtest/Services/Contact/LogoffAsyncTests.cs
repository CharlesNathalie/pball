namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LogoffAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    orderby c.ContactID
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        LoginModel loginModel = new LoginModel()
                        {
                            LoginEmail = contact.LoginEmail,
                            Password = contact.LastName,
                        };

                        var actionRes = await ContactService.LoginAsync(loginModel);
                        Contact? contactRet = await DoOKTestReturnContactAsync(actionRes);

                        Assert.NotNull(contactRet);
                        if (contactRet != null)
                        {
                            Assert.Equal(contact.ContactID, contactRet.ContactID);

                            Assert.NotNull(UserService);
                            if (UserService != null)
                            {
                                UserService.User = contact;

                                var actionRes2 = await ContactService.LogoffAsync(contact.ContactID);
                                bool? boolRet = await DoOKTestReturnBoolAsync(actionRes2);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);

                                Assert.NotNull(LoggedInService);
                                if (LoggedInService != null)
                                {
                                    Contact? contactLoggedIn = (from c in LoggedInService.LoggedInContactList
                                                                where c.ContactID == contact.ContactID
                                                                select c).FirstOrDefault();
                                    Assert.Null(contactLoggedIn);
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
    public async Task LogoffAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
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
                        UserService.User = null; // contact;
                    }

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        LoginModel loginModel = new LoginModel()
                        {
                            LoginEmail = contact.LoginEmail,
                            Password = contact.LastName,
                        };

                        var actionRes = await ContactService.LoginAsync(loginModel);
                        Contact? contactRet = await DoOKTestReturnContactAsync(actionRes);

                        Assert.NotNull(contactRet);
                        if (contactRet != null)
                        {
                            var actionRes2 = await ContactService.LogoffAsync(contact.ContactID);
                            bool? boolRet = await DoBadRequestBoolTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes2);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);

                            Assert.NotNull(LoggedInService);
                            if (LoggedInService != null)
                            {
                                Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact.ContactID).Any());
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
    public async Task LogoffAsync_ContactID_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    orderby c.ContactID
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    LoginModel loginModel = new LoginModel()
                    {
                        LoginEmail = contact.LoginEmail,
                        Password = contact.LastName,
                    };

                    if (ContactService != null)
                    {
                        var actionLoginRes = await ContactService.LoginAsync(loginModel);
                        Contact? contactRet = await DoOKTestReturnContactAsync(actionLoginRes);

                        Assert.NotNull(contactRet);
                        if (contactRet != null)
                        {
                            Assert.NotNull(LoggedInService);
                            if (LoggedInService != null)
                            {
                                Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact.ContactID).Any());

                                int ContactID = 0;

                                var actionRes = await ContactService.LogoffAsync(ContactID);
                                bool? boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "ContactID"), actionRes);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);

                                Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact.ContactID).Any());

                                ContactID = 10000;

                                actionRes = await ContactService.LogoffAsync(ContactID);
                                boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", ContactID.ToString()), actionRes);
                                Assert.NotNull(boolRet);
                                Assert.True(boolRet);

                                Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact.ContactID).Any());
                            }
                        }
                    }
                }
            }
        }
    }
}

