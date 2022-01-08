namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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
                contact2.FirstName = "CharlesNew";
                contact2.FirstName = "LeBlancNew";
                contact2.FirstName = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRes);
                if (contactRet2 != null)
                {
                    Assert.Equal(contact2.ContactID, contactRet2.ContactID);
                    Assert.Equal(contact2.FirstName, contactRet2.FirstName);
                    Assert.Equal(contact2.LastName, contactRet2.LastName);
                    Assert.Equal(contact2.Initial, contactRet2.Initial);
                    Assert.Equal(contact2.PlayerLevel, contactRet2.PlayerLevel);
                    Assert.Empty(contactRet2.PasswordHash);
                    Assert.NotEmpty(contactRet2.Token);
                    Assert.Empty(contactRet2.ResetPasswordTempCode);
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
    public async Task ModifyContactAsync_Authorization_Error_Test(string culture)
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
                contact2.FirstName = "CharlesNew";
                contact2.FirstName = "LeBlancNew";
                contact2.FirstName = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                if (UserService != null)
                {
                    UserService.User = null;
                }

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_LoginEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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
                contact2.FirstName = "CharlesNew";
                contact2.LastName = "LeBlancNew";
                contact2.Initial = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                contact2.LoginEmail = "";

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
                Assert.True(boolRet);

                contact2.LoginEmail = "a@b.c";

                actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"), actionRes);
                Assert.True(boolRet);

                contact2.LoginEmail = "a".PadRight(101) + "@b.c";

                actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_FirstName_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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
                contact2.FirstName = "CharlesNew";
                contact2.LastName = "LeBlancNew";
                contact2.Initial = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                contact2.FirstName = "";

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "FirstName"), actionRes);
                Assert.True(boolRet);

                contact2.FirstName = "a".PadRight(101);

                actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "FirstName", "100"), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_LastName_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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
                contact2.FirstName = "CharlesNew";
                contact2.LastName = "LeBlancNew";
                contact2.Initial = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                contact2.LastName = "";

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LastName"), actionRes);
                Assert.True(boolRet);

                contact2.LastName = "a".PadRight(101);

                actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "LastName", "100"), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Initial_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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
                contact2.FirstName = "CharlesNew";
                contact2.LastName = "LeBlancNew";
                contact2.Initial = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                contact2.Initial = "a".PadRight(51);

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "Initial", "50"), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_PlayerLevel_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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
                contact2.FirstName = "CharlesNew";
                contact2.LastName = "LeBlancNew";
                contact2.Initial = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                contact2.PlayerLevel = 0.99D;

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
                Assert.True(boolRet);

                contact2.PlayerLevel = 5.1D;

                actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_CouldNotFind_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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
                contact2.FirstName = "CharlesNew";
                contact2.LastName = "LeBlancNew";
                contact2.Initial = "GNew";
                contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                contact2.ContactID = 10000;

                var actionRes = await ContactService.ModifyContactAsync(contact2);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contact2.ContactID.ToString()), actionRes);
                Assert.True(boolRet);
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_LoginEmail_AlreadyTaken_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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

            registerModel = await FillRegisterModelAsync();
            registerModel.FirstName = registerModel.FirstName + "New";
            registerModel.LoginEmail = "New" + registerModel.LoginEmail;
            actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contact2 != null)
            {
                if (contactRet2 != null)
                {
                    contact2.FirstName = "CharlesNew";
                    contact2.LastName = "LeBlancNew";
                    contact2.Initial = "GNew";
                    contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                    contact2.LoginEmail = contactRet2.LoginEmail;

                    var actionRes = await ContactService.ModifyContactAsync(contact2);
                    boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, "LoginEmail"), actionRes);
                    Assert.True(boolRet);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_FullName_AlreadyTaken_With_Initial_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
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

            registerModel = await FillRegisterModelAsync();
            registerModel.FirstName = "New" + registerModel.FirstName;
            registerModel.LoginEmail = "New" + registerModel.LoginEmail;
            actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contact2 != null)
            {
                if (contactRet2 != null)
                {
                    contact2.FirstName = contactRet2.FirstName;
                    contact2.LastName = contactRet2.LastName;
                    contact2.Initial = contactRet2.Initial;
                    contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                    string Initial = contact2.Initial == null ? "" : contact2.Initial.EndsWith(".") ? contact2.Initial.Substring(0, contact2.Initial.Length - 1) : contact2.Initial;
                    string FullName = $"{ contact2.FirstName } { Initial } { contact2.LastName }";

                    var actionRes = await ContactService.ModifyContactAsync(contact2);
                    boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, FullName), actionRes);
                    Assert.True(boolRet);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_FullName_AlreadyTaken_Without_Initial_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            registerModel.Initial = "";

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

            registerModel = await FillRegisterModelAsync();
            registerModel.Initial = "";
            registerModel.FirstName = "New" + registerModel.FirstName;
            registerModel.LoginEmail = "New" + registerModel.LoginEmail;
            actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contact2 != null)
            {
                if (contactRet2 != null)
                {
                    contact2.FirstName = contactRet2.FirstName;
                    contact2.LastName = contactRet2.LastName;
                    contact2.Initial = contactRet2.Initial;
                    contact2.PlayerLevel = contact2.PlayerLevel + 0.01D;

                    contact2.Initial = "";

                    string FullName = $"{ contact2.FirstName } { contact2.LastName }";

                    var actionRes = await ContactService.ModifyContactAsync(contact2);
                    boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, FullName), actionRes);
                    Assert.True(boolRet);
                }
            }
        }
    }
}

