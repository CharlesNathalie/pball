namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        boolRet = await DoRegisterTestAsync();
        Assert.True(boolRet);

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
    public async Task RegisterAsync_registerModel_null_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel = null;

            var actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ShouldNotBeNullOrEmpty, "registerModel"), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_LoginEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel.LoginEmail = "";

            var actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
            Assert.True(boolRet);

            registerModel.LoginEmail = "";

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
            Assert.True(boolRet);

            registerModel.LoginEmail = "a@b.c";

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "255"), actionRes);
            Assert.True(boolRet);

            registerModel.LoginEmail = "a".PadRight(256) + "@b.c";

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "255"), actionRes);
            Assert.True(boolRet);

            registerModel.LoginEmail = "aaaaaaaab.c";

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsNotAValidEmail, registerModel.LoginEmail), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_FirstName_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel.FirstName = "";

            var actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "FirstName"), actionRes);
            Assert.True(boolRet);

            registerModel.FirstName = "a".PadRight(101);

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "FirstName", "100"), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_LastName_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel.LastName = "";

            var actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LastName"), actionRes);
            Assert.True(boolRet);

            registerModel.LastName = "a".PadRight(101);

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "LastName", "100"), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_Initial_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel.Initial = "a".PadRight(51);

            var actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "Initial", "50"), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_PlayerLevel_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel.PlayerLevel = 0.9D;

            var actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
            Assert.True(boolRet);

            registerModel.PlayerLevel = 5.1D;

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_Password_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel.Password = "";

            var actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "Password"), actionRes);
            Assert.True(boolRet);

            registerModel.Password = "a".PadRight(51);

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "Password", "50"), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_LoginEmail_AlreadyTaken_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            var actionRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                registerModel.LoginEmail = contact.LoginEmail;
            }

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, registerModel.LoginEmail), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_FullName_AlreadyTaken_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            var actionRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                registerModel.FirstName = contact.FirstName;
                registerModel.LastName = contact.LastName;
                registerModel.Initial = contact.Initial;
            }

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, registerModel.LoginEmail), actionRes);
            Assert.True(boolRet);
        }

        boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        Assert.NotNull(ContactService);

        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModel();

            registerModel.Initial = "";

            var actionRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                registerModel.FirstName = contact.FirstName;
                registerModel.LastName = contact.LastName;
                registerModel.Initial = contact.Initial;
            }

            actionRes = await ContactService.RegisterAsync(registerModel);
            boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, registerModel.LoginEmail), actionRes);
            Assert.True(boolRet);
        }
    }
}

