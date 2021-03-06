namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        RegisterModel registerModel = await FillRegisterModelAsync();

        Contact? contact = await DoRegisterTestAsync(registerModel);
        Assert.NotNull(contact);

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                if (contact != null)
                {
                    Contact? contactToDelete = (from c in db.Contacts
                                                where c.ContactID == contact.ContactID
                                                select c).FirstOrDefault();

                    Assert.NotNull(contactToDelete);
                    if (contactToDelete != null)
                    {
                        try
                        {
                            db.Contacts.Remove(contactToDelete);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Assert.True(false, ex.Message);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_LoginEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        RegisterModel? registerModel = await FillRegisterModelAsync();

        registerModel.LoginEmail = "";

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            var actionRes = await ContactService.RegisterAsync(registerModel);
            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
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

        RegisterModel? registerModel = await FillRegisterModelAsync();

        registerModel.FirstName = "";

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            var actionRes = await ContactService.RegisterAsync(registerModel);
            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "FirstName"), actionRes);
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

        RegisterModel? registerModel = await FillRegisterModelAsync();

        registerModel.LastName = "";

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            var actionRes = await ContactService.RegisterAsync(registerModel);
            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LastName"), actionRes);
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

        RegisterModel? registerModel = await FillRegisterModelAsync();

        registerModel.Initial = "a".PadRight(51);

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            var actionRes = await ContactService.RegisterAsync(registerModel);
            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "Initial", "50"), actionRes);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_PlayerLevel_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            RegisterModel? registerModel = await FillRegisterModelAsync();

            registerModel.PlayerLevel = 0.9D;

            var actionRes = await ContactService.RegisterAsync(registerModel);
            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
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

        RegisterModel? registerModel = await FillRegisterModelAsync();

        registerModel.Password = "";

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            var actionRes = await ContactService.RegisterAsync(registerModel);
            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "Password"), actionRes);
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
                    RegisterModel? registerModel = await FillRegisterModelAsync();
                    registerModel.LoginEmail = contact.LoginEmail;

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.RegisterAsync(registerModel);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, registerModel.LoginEmail), actionRes);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_FullName_With_Initial_AlreadyTaken_Error_Test(string culture)
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
                                    where (c.Initial != null
                                    && c.Initial != "")
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    RegisterModel? registerModel = await FillRegisterModelAsync();

                    registerModel.FirstName = contact.FirstName;
                    registerModel.LastName = contact.LastName;
                    registerModel.Initial = contact.Initial;

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        string Initial = registerModel.Initial.EndsWith(".") ? registerModel.Initial.Substring(0, registerModel.Initial.Length - 1) : registerModel.Initial;
                        string FullName = $"{ registerModel.FirstName } { Initial } { registerModel.LastName }";

                        var actionRes = await ContactService.RegisterAsync(registerModel);
                        bool? boolRet2 = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, FullName), actionRes);
                        Assert.True(boolRet2);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_FullName_Without_Initial_AlreadyTaken_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(ContactService);
        if (ContactService != null)
        {
            Assert.NotNull(db);
            if (db != null)
            {
                Assert.NotNull(db.Contacts);
                if (db.Contacts != null)
                {
                    Contact? contact = (from c in db.Contacts
                                        orderby c.ContactID
                                        where (c.Initial == null
                                        || c.Initial == "")
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        RegisterModel? registerModel = await FillRegisterModelAsync();

                        registerModel.FirstName = contact.FirstName;
                        registerModel.LastName = contact.LastName;
                        registerModel.Initial = "";

                        string FullName = $"{ registerModel.FirstName } { registerModel.LastName }";

                        var actionRes = await ContactService.RegisterAsync(registerModel);
                        bool? boolRet2 = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, FullName), actionRes);
                        Assert.True(boolRet2);
                    }
                }
            }
        }
    }
}

