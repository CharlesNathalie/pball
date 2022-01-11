namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Good_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    string FirstName = contact.FirstName;
                    string LastName = contact.LastName;
                    string Initial = contact.Initial;
                    double PlayerLevel = contact.PlayerLevel;

                    contact.FirstName = "FirstNameNew";
                    contact.FirstName = "LastNameNew";
                    contact.FirstName = "InitNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        Contact? contactRet2 = await DoOKTestReturnContactAsync(actionRes);
                        if (contactRet2 != null)
                        {
                            Assert.Equal(contact.ContactID, contactRet2.ContactID);
                            Assert.Equal(contact.FirstName, contactRet2.FirstName);
                            Assert.Equal(contact.LastName, contactRet2.LastName);
                            Assert.Equal(contact.Initial, contactRet2.Initial);
                            Assert.Equal(contact.PlayerLevel, contactRet2.PlayerLevel);
                            Assert.Empty(contactRet2.PasswordHash);
                            Assert.NotEmpty(contactRet2.Token);
                            Assert.Empty(contactRet2.ResetPasswordTempCode);
                        }

                        contact.FirstName = FirstName;
                        contact.LastName = LastName;
                        contact.Initial = Initial;
                        contact.PlayerLevel = PlayerLevel;

                        actionRes = await ContactService.ModifyContactAsync(contact);
                        contactRet2 = await DoOKTestReturnContactAsync(actionRes);
                        if (contactRet2 != null)
                        {
                            Assert.Equal(contact.ContactID, contactRet2.ContactID);
                            Assert.Equal(contact.FirstName, contactRet2.FirstName);
                            Assert.Equal(contact.LastName, contactRet2.LastName);
                            Assert.Equal(contact.Initial, contactRet2.Initial);
                            Assert.Equal(contact.PlayerLevel, contactRet2.PlayerLevel);
                            Assert.Empty(contactRet2.PasswordHash);
                            Assert.NotEmpty(contactRet2.Token);
                            Assert.Empty(contactRet2.ResetPasswordTempCode);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Authorization_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    contact.FirstName = "FirstNameNew";
                    contact.FirstName = "LastNameNew";
                    contact.FirstName = "InitNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = null;
                    }

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        bool? boolRet = await DoBadRequestContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_LoginEmail_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    contact.FirstName = "FirstNameNew";
                    contact.LastName = "LastNameNew";
                    contact.Initial = "InitNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    contact.LoginEmail = "";

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
                        Assert.True(boolRet);

                        contact.LoginEmail = "a@b.c";

                        actionRes = await ContactService.ModifyContactAsync(contact);
                        boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"), actionRes);
                        Assert.True(boolRet);

                        contact.LoginEmail = "a".PadRight(101) + "@b.c";

                        actionRes = await ContactService.ModifyContactAsync(contact);
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
    public async Task ModifyContactAsync_FirstName_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    contact.FirstName = "CharlesNew";
                    contact.LastName = "LeBlancNew";
                    contact.Initial = "GNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    contact.FirstName = "";

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "FirstName"), actionRes);
                        Assert.True(boolRet);

                        contact.FirstName = "a".PadRight(101);

                        actionRes = await ContactService.ModifyContactAsync(contact);
                        boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "FirstName", "100"), actionRes);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_LastName_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    contact.FirstName = "CharlesNew";
                    contact.LastName = "LeBlancNew";
                    contact.Initial = "GNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    contact.LastName = "";

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._IsRequired, "LastName"), actionRes);
                        Assert.True(boolRet);

                        contact.LastName = "a".PadRight(101);

                        actionRes = await ContactService.ModifyContactAsync(contact);
                        boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "LastName", "100"), actionRes);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_Initial_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    contact.FirstName = "CharlesNew";
                    contact.LastName = "LeBlancNew";
                    contact.Initial = "GNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    contact.Initial = "a".PadRight(51);

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._MaxLengthIs_, "Initial", "50"), actionRes);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_PlayerLevel_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }
                }

                if (contact != null)
                {
                    contact.FirstName = "CharlesNew";
                    contact.LastName = "LeBlancNew";
                    contact.Initial = "GNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    contact.PlayerLevel = 0.99D;

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
                        Assert.True(boolRet);

                        contact.PlayerLevel = 5.1D;

                        actionRes = await ContactService.ModifyContactAsync(contact);
                        boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._ValueShouldBeBetween_And_, "PlayerLevel", "1.0", "5.0"), actionRes);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_CouldNotFind_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    contact.FirstName = "CharlesNew";
                    contact.LastName = "LeBlancNew";
                    contact.Initial = "GNew";
                    contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                    contact.ContactID = -1;

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.ModifyContactAsync(contact);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", contact.ContactID.ToString()), actionRes);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyContactAsync_LoginEmail_AlreadyTaken_Error_Test(string culture)
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
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    Contact? contact2 = (from c in db.Contacts
                                         orderby c.ContactID
                                         select c).Skip(1).FirstOrDefault();
                    Assert.NotNull(contact2);
                    if (contact2 != null)
                    {

                        contact.FirstName = "CharlesNew";
                        contact.LastName = "LeBlancNew";
                        contact.Initial = "GNew";
                        contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                        contact.LoginEmail = contact2.LoginEmail;

                        Assert.NotNull(ContactService);
                        if (ContactService != null)
                        {
                            var actionRes = await ContactService.ModifyContactAsync(contact);
                            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, "LoginEmail"), actionRes);
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
    public async Task ModifyContactAsync_FullName_AlreadyTaken_With_Initial_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    where string.IsNullOrWhiteSpace(c.Initial)
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    Contact? contact2 = (from c in db.Contacts
                                         where !string.IsNullOrWhiteSpace(c.Initial) 
                                         select c).FirstOrDefault();

                    Assert.NotNull(contact2);
                    if (contact2 != null)
                    {
                        contact.FirstName = contact2.FirstName;
                        contact.LastName = contact2.LastName;
                        contact.Initial = contact2.Initial;
                        contact.PlayerLevel = contact.PlayerLevel + 0.01D;

                        string Initial = contact.Initial == null ? "" : contact.Initial.EndsWith(".") ? contact.Initial.Substring(0, contact.Initial.Length - 1) : contact.Initial;
                        string FullName = $"{ contact.FirstName } { Initial } { contact.LastName }";

                        Assert.NotNull(ContactService);
                        if (ContactService != null)
                        {
                            var actionRes = await ContactService.ModifyContactAsync(contact);
                            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, FullName), actionRes);
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
    public async Task ModifyContactAsync_FullName_AlreadyTaken_Without_Initial_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    where !string.IsNullOrWhiteSpace(c.Initial)
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    Assert.NotNull(UserService);
                    if (UserService != null)
                    {
                        UserService.User = contact;
                    }

                    Contact? contact2 = (from c in db.Contacts
                                         where string.IsNullOrWhiteSpace(c.Initial)
                                         select c).Skip(1).FirstOrDefault();

                    Assert.NotNull(contact2);
                    if (contact2 != null)
                    {
                        contact.FirstName = contact2.FirstName;
                        contact.LastName = contact2.LastName;
                        contact.Initial = contact2.Initial;
                        contact.PlayerLevel = contact2.PlayerLevel + 0.01D;

                        contact.Initial = "";

                        string FullName = $"{ contact.FirstName } { contact.LastName }";

                        Assert.NotNull(ContactService);
                        if (ContactService != null)
                        {
                            var actionRes = await ContactService.ModifyContactAsync(contact);
                            bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes._AlreadyTaken, FullName), actionRes);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}
