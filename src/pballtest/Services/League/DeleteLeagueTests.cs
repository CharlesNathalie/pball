namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

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

            if (LeagueService != null)
            {
                League league = await FillLeague();
                Assert.NotEmpty(league.LeagueName);

                var actionRes = await LeagueService.AddLeagueAsync(league);
                League? leagueRet = await DoOKTestReturnLeagueAsync(actionRes);
                Assert.NotNull(leagueRet);

                if (leagueRet != null)
                {
                    var actionDeleteRes = await LeagueService.DeleteLeagueAsync(leagueRet.LeagueID);
                    League? leagueRet2 = await DoOKTestReturnLeagueAsync(actionDeleteRes);
                    Assert.NotNull(leagueRet2);
                    if (leagueRet2 != null)
                    {
                        Assert.True(leagueRet2.LeagueID > 0);
                        Assert.True(leagueRet2.Removed);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

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

            if (LeagueService != null)
            {
                var actionRes = await LeagueService.AddLeagueAsync(new League());
                boolRet = await DoBadRequestLeagueTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _LeagueServiceSetupAsync(culture));

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

            if (LeagueService != null)
            {
                League league = await FillLeague();

                league.LeagueID = 0;

                var actionRes = await LeagueService.DeleteLeagueAsync(league.LeagueID);
                boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                league.LeagueID = 10000;

                actionRes = await LeagueService.DeleteLeagueAsync(league.LeagueID);
                boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", league.LeagueID.ToString()), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}


