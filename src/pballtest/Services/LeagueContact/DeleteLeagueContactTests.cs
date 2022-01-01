namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_Good_Test(string culture)
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

                if (leagueContactRet != null)
                {
                    var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactRet.LeagueContactID);
                    LeagueContact? leagueContactRet2 = await DoOKTestReturnLeagueContactAsync(actionDeleteRes);
                    if (leagueContactRet2 != null)
                    {
                        Assert.True(leagueContactRet2.LeagueContactID > 0);
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
                var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(1);
                boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionDeleteRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_LeagueContactID_Error_Test(string culture)
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

                if (leagueContactRet != null)
                {
                    leagueContactRet.LeagueContactID = 0;

                    var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactRet.LeagueContactID);
                    boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueContactID"), actionDeleteRes);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);

                    leagueContactRet.LeagueContactID = 10000;

                    actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactRet.LeagueContactID);
                    boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "LeagueContactID", leagueContactRet.LeagueContactID.ToString()), actionDeleteRes);
                    Assert.NotNull(boolRet);
                    Assert.True(boolRet);
                }
            }
        }
    }
}

