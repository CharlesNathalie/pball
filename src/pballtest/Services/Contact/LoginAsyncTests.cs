namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Good_Test(string culture)
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
                    LoginModel loginModel = new LoginModel()
                    {
                        LoginEmail = contact.LoginEmail,
                        Password = contact.LastName,
                    };

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.LoginAsync(loginModel);
                        contact = await DoOKTestReturnContactAsync(actionRes);
                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.Empty(contact.PasswordHash);
                            Assert.NotEmpty(contact.Token);
                            Assert.Empty(contact.ResetPasswordTempCode);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_LoginEmail_Error_Test(string culture)
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
                    LoginModel loginModel = new LoginModel()
                    {
                        LoginEmail = "",
                        Password = contact.LastName,
                    };

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {

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
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Password_Error_Test(string culture)
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
                    LoginModel loginModel = new LoginModel()
                    {
                        LoginEmail = contact.LoginEmail,
                        Password = "",
                    };

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
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
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_NotFound_Error_Test(string culture)
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
                    LoginModel loginModel = new LoginModel()
                    {
                        LoginEmail = "NotFound@gmail.com",
                        Password = contact.LastName,
                    };

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.LoginAsync(loginModel);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail), actionRes);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginAsync_Password_NotGood_Error_Test(string culture)
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
                    LoginModel loginModel = new LoginModel()
                    {
                        LoginEmail = contact.LoginEmail,
                        Password = "NotFoundPassword",
                    };

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.LoginAsync(loginModel);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail), actionRes);
                        Assert.True(boolRet);

                    }
                }
            }
        }
    }
}

