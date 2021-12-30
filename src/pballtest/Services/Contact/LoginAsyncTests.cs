namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        if (ContactService != null)
        {
            if (Configuration != null)
            {
                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = Configuration["Password"],
                };

                var actionRes = await ContactService.LoginAsync(loginModel);
                Contact? contact = await DoOKTestReturnContactAsync(actionRes);
                Assert.NotNull(contact);
                if (contact != null)
                {
                    Assert.Empty(contact.PasswordHash);
                    Assert.NotEmpty(contact.Token);
                }
            }
        }

        if (db != null)
        {
            List<Contact> contactList = (from c in db.Contacts
                                         select c).ToList();

            Assert.NotNull(contactList);
            Assert.NotEmpty(contactList);
            Assert.Single(contactList);
        }

    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_LoginEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        if (ContactService != null)
        {
            if (Configuration != null)
            {
                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = "",
                    Password = Configuration["Password"],
                };

                var actionRes = await ContactService.LoginAsync(loginModel);
                bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
                Assert.True(boolRet);

                loginModel.LoginEmail = "a@b.c";

                actionRes = await ContactService.LoginAsync(loginModel);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"), actionRes);
                Assert.True(boolRet);

                loginModel.LoginEmail = "a".PadRight(101) + "@b.c";

                actionRes = await ContactService.LoginAsync(loginModel);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Password_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        if (ContactService != null)
        {
            if (Configuration != null)
            {
                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = "",
                };

                var actionRes = await ContactService.LoginAsync(loginModel);
                bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "Password"), actionRes);
                Assert.True(boolRet);

                loginModel.Password = "a".PadRight(51);

                actionRes = await ContactService.LoginAsync(loginModel);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "Password", "50"), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_NotFound_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        if (ContactService != null)
        {
            if (Configuration != null)
            {
                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = "NotFound" + Configuration["LoginEmail"],
                    Password = Configuration["LoginEmail"],
                };

                var actionRes = await ContactService.LoginAsync(loginModel);
                bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Password_NotGood_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        if (ContactService != null)
        {
            if (Configuration != null)
            {
                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = "NotFound" + Configuration["LoginEmail"],
                };

                var actionRes = await ContactService.LoginAsync(loginModel);
                bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail), actionRes);
                Assert.True(boolRet);
            }
        }
    }
}

