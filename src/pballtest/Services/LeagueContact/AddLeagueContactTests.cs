namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

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
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (LeagueContactService != null)
            {
                LeagueContact leagueContact = await FillLeagueContact();
                Assert.True(leagueContact.LeagueID > 0);
                Assert.True(leagueContact.ContactID > 0);

                var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
                if (leagueContactRet != null)
                {
                    Assert.True(leagueContactRet.LeagueContactID > 0);
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
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (UserService != null)
            {
                UserService.User = null;
            }

            if (LeagueContactService != null)
            {
                var actionRes = await LeagueContactService.AddLeagueContactAsync(new LeagueContact());
                boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_LeagueContactID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

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
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (LeagueContactService != null)
            {
                LeagueContact leagueContact = await FillLeagueContact();
                Assert.True(leagueContact.LeagueID > 0);
                Assert.True(leagueContact.ContactID > 0);

                leagueContact.LeagueContactID = 1;

                var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._ShouldBeEqualTo_, "LeagueContactID", "0"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

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
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (LeagueContactService != null)
            {
                LeagueContact leagueContact = await FillLeagueContact();
                Assert.True(leagueContact.LeagueID > 0);
                Assert.True(leagueContact.ContactID > 0);

                leagueContact.LeagueID = 0;

                var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionRes);
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
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_ContactID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

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
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (LeagueContactService != null)
            {
                LeagueContact leagueContact = await FillLeagueContact();
                Assert.True(leagueContact.LeagueID > 0);
                Assert.True(leagueContact.ContactID > 0);

                leagueContact.ContactID = 0;

                var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "ContactID"), actionRes);
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
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_AlreadyExist_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

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
            contact = await DoOKTestReturnContactAsync(actionLoginRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                if (UserService != null)
                {
                    UserService.User = contact;
                }
                Assert.True(contact.ContactID > 0);
                Assert.Empty(contact.PasswordHash);
                Assert.NotEmpty(contact.Token);
            }

            if (LeagueContactService != null)
            {
                LeagueContact leagueContact = await FillLeagueContact();
                Assert.True(leagueContact.LeagueID > 0);
                Assert.True(leagueContact.ContactID > 0);

                var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
                if (leagueContactRet != null)
                {
                    Assert.True(leagueContactRet.LeagueContactID > 0);
                }

                actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._AlreadyExist, "LeagueContact"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

