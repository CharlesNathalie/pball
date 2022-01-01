namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LogoffAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

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
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
                Assert.Empty(contact2.PasswordHash);
                Assert.NotEmpty(contact2.Token);
            }

            if (LoggedInService != null)
            {
                Assert.NotEmpty(LoggedInService.LoggedInContactList);

                if (contact2 != null)
                {
                    var actionRes2 = await ContactService.LogoffAsync(contact2.ContactID);
                    boolRet = await DoOKTestReturnBoolAsync(actionRes2);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);

                    Assert.False(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact2.ContactID).Any());
                    if (UserService != null)
                    {
                        Assert.Null(UserService.User);
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
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
                Assert.Empty(contact2.PasswordHash);
                Assert.NotEmpty(contact2.Token);
            }

            if (LoggedInService != null)
            {
                Assert.NotEmpty(LoggedInService.LoggedInContactList);

                if (contact2 != null)
                {
                    if (UserService != null)
                    {
                        UserService.User = null;
                    }

                    var actionRes2 = await ContactService.LogoffAsync(contact2.ContactID);
                    boolRet = await DoBadRequestBoolTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes2);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);

                    Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact2.ContactID).Any());
                    if (UserService != null)
                    {
                        Assert.Null(UserService.User);
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
            Contact? contact2 = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact2);

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact2;
                }
                Assert.True(contact2.ContactID > 0);
                Assert.Empty(contact2.PasswordHash);
                Assert.NotEmpty(contact2.Token);
            }

            if (LoggedInService != null)
            {
                Assert.NotEmpty(LoggedInService.LoggedInContactList);

                if (contact2 != null)
                {
                    contact2.ContactID = 0;

                    var actionRes2 = await ContactService.LogoffAsync(contact2.ContactID);
                    boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "ContactID"), actionRes2);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);

                    Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact2.ContactID).Any());
                    if (UserService != null)
                    {
                        Assert.NotNull(UserService.User);
                    }

                    contact2.ContactID = 10000;

                    actionRes2 = await ContactService.LogoffAsync(contact2.ContactID);
                    boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contact2.ContactID.ToString()), actionRes2);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);

                    Assert.False(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact2.ContactID).Any());
                    if (UserService != null)
                    {
                        Assert.NotNull(UserService.User);
                    }
                }
            }
        }
    }
}

