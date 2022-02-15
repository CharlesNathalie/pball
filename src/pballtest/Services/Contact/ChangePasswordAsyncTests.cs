namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Random random = new Random();

        string TempCode = $"{ random.Next(1000, 9999) }";

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
                    contact.ResetPasswordTempCode = TempCode;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Assert.True(false, ex.Message);
                    }

                    Assert.NotNull(ScrambleService);
                    if (ScrambleService != null)
                    {
                        ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                        {
                            LoginEmail = contact.LoginEmail,
                            FirstName = contact.FirstName,
                            Password = "NewPassword",
                            TempCode = TempCode,
                        };

                        Assert.NotNull(ContactService);
                        if (ContactService != null)
                        {
                            var actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                            bool? boolRet = await DoOKTestReturnBoolAsync(actionRes);
                            Assert.True(boolRet);

                            LoginModel loginModel = new LoginModel()
                            {
                                LoginEmail = contact.LoginEmail,
                                Password = "NewPassword",
                            };

                            var actionLoginRes = await ContactService.LoginAsync(loginModel);
                            Contact? contact3 = await DoOKTestReturnContactAsync(actionLoginRes);
                            if (contact3 != null)
                            {
                                Assert.Empty(contact3.PasswordHash);
                                Assert.Empty(contact3.ResetPasswordTempCode);
                                Assert.NotEmpty(contact3.Token);
                            }

                            contact.ResetPasswordTempCode = TempCode;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Assert.True(false, ex.Message);
                            }

                            changePasswordModel = new ChangePasswordModel()
                            {
                                LoginEmail = contact.LoginEmail,
                                FirstName = contact.FirstName,
                                Password = contact.LastName,
                                TempCode = TempCode,
                            };

                            actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                            boolRet = await DoOKTestReturnBoolAsync(actionRes);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_LoginEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Random random = new Random();

        string TempCode = $"{ random.Next(1000, 9999) }";

        ChangePasswordModel changePasswordModel = new ChangePasswordModel()
        {
            LoginEmail = "",
            FirstName = "SomeName",
            Password = "SomePassword",
            TempCode = TempCode,
        };

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            var actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
            bool? boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            changePasswordModel.LoginEmail = "NotExistLoginEmail";

            actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
            boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "LoginEmail", changePasswordModel.LoginEmail), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Password_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Random random = new Random();

        string TempCode = $"{ random.Next(1000, 9999) }";

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db != null && db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {

                    ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                    {
                        LoginEmail = contact.LoginEmail,
                        FirstName = contact.FirstName,
                        Password = "",
                        TempCode = TempCode,
                    };

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                        bool? boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "Password"), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                        changePasswordModel.Password = "a".PadRight(51);

                        actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                        boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._MaxLengthIs_, "Password", "50"), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}

