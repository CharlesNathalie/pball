namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            Contact? contact = await DoRegisterTestAsync(registerModel);
            Assert.NotNull(contact);

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionRes);
            Assert.NotNull(contact2);
            if (contact2 != null)
            {
                Assert.Empty(contact2.PasswordHash);
                Assert.NotEmpty(contact2.Token);
            }

            Random random = new Random();

            string TempCode = $"{ random.Next(1000, 9999) }";

            if (db != null)
            {
                if (contact2 != null)
                {
                    Contact? contactToAddTempCode = (from c in db.Contacts
                                                     where c.ContactID == contact2.ContactID
                                                     select c).FirstOrDefault();

                    Assert.NotNull(contactToAddTempCode);

                    if (contactToAddTempCode != null)
                    {
                        contactToAddTempCode.ResetPasswordTempCode = TempCode;
                    }

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Assert.True(false, ex.Message);
                    }
                }
            }

            ChangePasswordModel changePasswordModel = new ChangePasswordModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = $"{ registerModel.Password }New",
                TempCode = TempCode,
            };

            var actionRes2 = await ContactService.ChangePasswordAsync(changePasswordModel);
            boolRet = await DoOKTestReturnBoolAsync(actionRes2);
            Assert.True(boolRet);

            loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = $"{ registerModel.Password }New",
            };

            var actionLoginRes = await ContactService.LoginAsync(loginModel);
            Contact? contact3 = await DoOKTestReturnContactAsync(actionLoginRes);
            if (contact3 != null)
            {
                Assert.Empty(contact3.PasswordHash);
                Assert.NotEmpty(contact3.Token);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_LoginEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            Contact? contact = await DoRegisterTestAsync(registerModel);
            Assert.NotNull(contact);

            LoginModel loginModel = new LoginModel()
            {
                LoginEmail = registerModel.LoginEmail,
                Password = registerModel.Password,
            };

            var actionRes = await ContactService.LoginAsync(loginModel);
            Contact? contact2 = await DoOKTestReturnContactAsync(actionRes);
            Assert.NotNull(contact2);
            if (contact2 != null)
            {
                Assert.Empty(contact2.PasswordHash);
                Assert.NotEmpty(contact2.Token);
            }

            Random random = new Random();

            string TempCode = $"{ random.Next(1000, 9999) }";

            if (db != null)
            {
                if (contact2 != null)
                {
                    Contact? contactToAddTempCode = (from c in db.Contacts
                                                     where c.ContactID == contact2.ContactID
                                                     select c).FirstOrDefault();

                    Assert.NotNull(contactToAddTempCode);

                    if (contactToAddTempCode != null)
                    {
                        contactToAddTempCode.ResetPasswordTempCode = TempCode;
                    }

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Assert.True(false, ex.Message);
                    }
                }
            }

            ChangePasswordModel changePasswordModel = new ChangePasswordModel()
            {
                LoginEmail = "",
                Password = $"{ registerModel.Password }New",
                TempCode = TempCode,
            };

            var actionRes2 = await ContactService.ChangePasswordAsync(changePasswordModel);
            boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes2);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            changePasswordModel.LoginEmail = "NotExist" + registerModel.LoginEmail;

            actionRes2 = await ContactService.ChangePasswordAsync(changePasswordModel);
            boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "LoginEmail", changePasswordModel.LoginEmail), actionRes2);
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

        if (ContactService != null)
        {
            bool? boolRet = await ClearAllContactsFromDBAsync();
            Assert.True(boolRet);

            if (ContactService != null)
            {
                RegisterModel registerModel = await FillRegisterModelAsync();

                Contact? contact = await DoRegisterTestAsync(registerModel);
                Assert.NotNull(contact);

                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = registerModel.LoginEmail,
                    Password = registerModel.Password,
                };

                var actionRes = await ContactService.LoginAsync(loginModel);
                Contact? contact2 = await DoOKTestReturnContactAsync(actionRes);
                Assert.NotNull(contact2);
                if (contact2 != null)
                {
                    Assert.Empty(contact2.PasswordHash);
                    Assert.NotEmpty(contact2.Token);
                }

                Random random = new Random();

                string TempCode = $"{ random.Next(1000, 9999) }";

                if (db != null)
                {
                    if (contact2 != null)
                    {
                        Contact? contactToAddTempCode = (from c in db.Contacts
                                                         where c.ContactID == contact2.ContactID
                                                         select c).FirstOrDefault();

                        Assert.NotNull(contactToAddTempCode);

                        if (contactToAddTempCode != null)
                        {
                            contactToAddTempCode.ResetPasswordTempCode = TempCode;
                        }

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Assert.True(false, ex.Message);
                        }
                    }
                }
                
                ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                {
                    LoginEmail = registerModel.LoginEmail,
                    Password = "",
                    TempCode = TempCode,
                };
                var actionRes2 = await ContactService.ChangePasswordAsync(changePasswordModel);
                boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "Password"), actionRes2);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                changePasswordModel.Password = "a".PadRight(51);

                actionRes2 = await ContactService.ChangePasswordAsync(changePasswordModel);
                boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._MaxLengthIs_, "Password", "50"), actionRes2);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

