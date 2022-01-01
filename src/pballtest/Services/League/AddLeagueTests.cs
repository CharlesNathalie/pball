namespace pball.Services.Tests;

public partial class LeagueServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_Good_Test(string culture)
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
            }
        }

        if (db != null)
        {
            League? league = (from c in db.Leagues
                              select c).FirstOrDefault();

            Assert.NotNull(league);

            if (league != null)
            {
                Assert.True(league.LeagueID > 0);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_Authorization_Error_Test(string culture)
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
                League league = await FillLeague();
                Assert.NotEmpty(league.LeagueName);

                var actionRes = await LeagueService.AddLeagueAsync(league);
                boolRet = await DoBadRequestLeagueTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_LeagueName_Good_Test(string culture)
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

                league.LeagueName = "";

                var actionRes = await LeagueService.AddLeagueAsync(league);
                boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._IsRequired, "LeagueName"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueAsync_LeagueName_AlreadyTaken_Good_Test(string culture)
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

                actionRes = await LeagueService.AddLeagueAsync(league);
                boolRet = await DoBadRequestLeagueTestAsync(string.Format(PBallRes._AlreadyTaken, "LeagueName"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

