namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteContactAsync_Good_Test(string culture)
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

            if (contact2 != null)
            {
                var actionDeleteRes = await ContactService.DeleteContactAsync(contact2.ContactID);
                Contact? contact3 = await DoOKTestReturnContactAsync(actionDeleteRes);
                Assert.NotNull(contact3);

                if (contact3 != null)
                {
                    Assert.True(contact3.ContactID > 0);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteContactAsync_Authorization_Error_Test(string culture)
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
            }

            if (contact2 != null)
            {
                if (UserService != null)
                {
                    UserService.User = null;
                }

                var actionDeleteRes = await ContactService.DeleteContactAsync(contact2.ContactID);
                boolRet = await DoBadRequestContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionDeleteRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteContactAsync_CouldNotFindContact_Error_Test(string culture)
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
            }

            if (contact2 != null)
            {
                contact2.ContactID = 10000;

                var actionDeleteRes = await ContactService.DeleteContactAsync(contact2.ContactID);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contact2.ContactID.ToString()), actionDeleteRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

