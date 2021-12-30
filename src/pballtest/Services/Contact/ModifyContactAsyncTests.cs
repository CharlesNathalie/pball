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
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.FirstName = "LeBlancNew";
                contactRet.FirstName = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
                Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRes);
                if (contactRet2 != null)
                {
                    Assert.Equal(contactRet.ContactID, contactRet2.ContactID);
                    Assert.Equal(contactRet.FirstName, contactRet2.FirstName);
                    Assert.Equal(contactRet.LastName, contactRet2.LastName);
                    Assert.Equal(contactRet.Initial, contactRet2.Initial);
                    Assert.Equal(contactRet.PlayerLevel, contactRet2.PlayerLevel);
                    Assert.Empty(contactRet2.PasswordHash);
                    Assert.Empty(contactRet2.Token);
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
    public async Task ModifyContactAsync_Authorized_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.LastName = "LeBlancNew";
                contactRet.Initial = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                if (LoggedInService != null)
                {
                    if (LoggedInService.LoggedInContactInfo != null)
                    {
                        LoggedInService.LoggedInContactInfo.LoggedInContact = null;
                    }
                }

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
                boolRet = await DoBadRequestContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
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
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.LastName = "LeBlancNew";
                contactRet.Initial = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                contactRet.LoginEmail = "";

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
                Assert.True(boolRet);

                contactRet.LoginEmail = "a@b.c";

                actionRes = await ContactService.ModifyContactAsync(contactRet);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"), actionRes);
                Assert.True(boolRet);

                contactRet.LoginEmail = "a".PadRight(101) + "@b.c";

                actionRes = await ContactService.ModifyContactAsync(contactRet);
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
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.LastName = "LeBlancNew";
                contactRet.Initial = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                contactRet.FirstName = "";

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "FirstName"), actionRes);
                Assert.True(boolRet);

                contactRet.FirstName = "a".PadRight(101);

                actionRes = await ContactService.ModifyContactAsync(contactRet);
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
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.LastName = "LeBlancNew";
                contactRet.Initial = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                contactRet.LastName = "";

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LastName"), actionRes);
                Assert.True(boolRet);

                contactRet.LastName = "a".PadRight(101);

                actionRes = await ContactService.ModifyContactAsync(contactRet);
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
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.LastName = "LeBlancNew";
                contactRet.Initial = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                contactRet.Initial = "a".PadRight(51);

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
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
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.LastName = "LeBlancNew";
                contactRet.Initial = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                contactRet.PlayerLevel = 0.99D;

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
                Assert.True(boolRet);

                contactRet.PlayerLevel = 5.1D;

                actionRes = await ContactService.ModifyContactAsync(contactRet);
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
            RegisterModel registerModel = await FillRegisterModel();
            Assert.NotEmpty(registerModel.FirstName);
            Assert.NotEmpty(registerModel.LastName);
            Assert.NotEmpty(registerModel.LoginEmail);

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                contactRet.FirstName = "CharlesNew";
                contactRet.LastName = "LeBlancNew";
                contactRet.Initial = "GNew";
                contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                contactRet.ContactID = 10000;

                var actionRes = await ContactService.ModifyContactAsync(contactRet);
                boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contactRet.ContactID.ToString()), actionRes);
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
            RegisterModel registerModel = await FillRegisterModel();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            registerModel = await FillRegisterModel();
            registerModel.FirstName = registerModel.FirstName + "New";
            registerModel.LoginEmail = "New" + registerModel.LoginEmail;
            actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                if (contactRet2 != null)
                {
                    contactRet.FirstName = "CharlesNew";
                    contactRet.LastName = "LeBlancNew";
                    contactRet.Initial = "GNew";
                    contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                    contactRet.LoginEmail = contactRet2.LoginEmail;

                    var actionRes = await ContactService.ModifyContactAsync(contactRet);
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
            RegisterModel registerModel = await FillRegisterModel();

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            registerModel = await FillRegisterModel();
            registerModel.FirstName = "New" + registerModel.FirstName;
            registerModel.LoginEmail = "New" + registerModel.LoginEmail;
            actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                if (contactRet2 != null)
                {
                    contactRet.FirstName = contactRet2.FirstName;
                    contactRet.LastName = contactRet2.LastName;
                    contactRet.Initial = contactRet2.Initial;
                    contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                    string Initial = contactRet.Initial == null ? "" : contactRet.Initial.EndsWith(".") ? contactRet.Initial.Substring(0, contactRet.Initial.Length - 1) : contactRet.Initial;
                    string FullName = $"{ contactRet.FirstName } { Initial } { contactRet.LastName }";

                    var actionRes = await ContactService.ModifyContactAsync(contactRet);
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
            RegisterModel registerModel = await FillRegisterModel();

            registerModel.Initial = "";

            var actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet = await DoOKTestReturnContactAsync(actionRegisterRes);

            registerModel = await FillRegisterModel();
            registerModel.Initial = "";
            registerModel.FirstName = "New" + registerModel.FirstName;
            registerModel.LoginEmail = "New" + registerModel.LoginEmail;
            actionRegisterRes = await ContactService.RegisterAsync(registerModel);
            Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRegisterRes);

            if (contactRet != null)
            {
                if (contactRet2 != null)
                {
                    contactRet.FirstName = contactRet2.FirstName;
                    contactRet.LastName = contactRet2.LastName;
                    contactRet.Initial = contactRet2.Initial;
                    contactRet.PlayerLevel = contactRet.PlayerLevel + 0.01D;

                    contactRet.Initial = "";

                    string FullName = $"{ contactRet.FirstName } { contactRet.LastName }";

                    var actionRes = await ContactService.ModifyContactAsync(contactRet);
                    boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, FullName), actionRes);
                    Assert.True(boolRet);
                }
            }
        }
    }
}

