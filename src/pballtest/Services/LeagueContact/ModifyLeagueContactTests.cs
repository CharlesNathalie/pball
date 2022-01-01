namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_Good_Test(string culture)
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
            Assert.True(leagueContact.ContactID > 0);

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
            Assert.NotNull(leagueContactRet);

                if (leagueContactRet != null)
                {
                    leagueContactRet.IsLeagueAdmin = !leagueContact.IsLeagueAdmin;

                    var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(leagueContactRet);
                    LeagueContact? leagueContactRet2 = await DoOKTestReturnLeagueContactAsync(actionModifyRes);
                    Assert.NotNull(leagueContactRet2);
                    if (leagueContactRet2 != null)
                    {
                        Assert.Equal(leagueContactRet.LeagueID, leagueContactRet2.LeagueID);
                        Assert.Equal(leagueContactRet.IsLeagueAdmin, leagueContactRet2.IsLeagueAdmin);
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
                var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(new LeagueContact());
                boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionModifyRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }

    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_LeagueContactID_Error_Test(string culture)
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
                Assert.True(leagueContact.ContactID > 0);

                var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
                LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
                Assert.NotNull(leagueContactRet);

                if (leagueContactRet != null)
                {
                    leagueContactRet.LeagueContactID = 0;

                    var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(leagueContactRet);
                    boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueContactID"), actionModifyRes);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);
                }
            }
        }
    }
}

