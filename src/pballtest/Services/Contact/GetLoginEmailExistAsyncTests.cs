namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLoginEmailExistAsync_Good_Test(string culture)
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
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        LoginEmailModel loginEmailModel = new LoginEmailModel()
                        {
                            LoginEmail = contact.LoginEmail
                        };

                        var actionRes = await ContactService.GetLoginEmailExistAsync(loginEmailModel);
                        bool? boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                        loginEmailModel.LoginEmail = $"Not{ contact.LoginEmail}";

                        actionRes = await ContactService.GetLoginEmailExistAsync(loginEmailModel);
                        boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.False(boolRet);

                        loginEmailModel.LoginEmail = "";

                        actionRes = await ContactService.GetLoginEmailExistAsync(loginEmailModel);
                        boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.False(boolRet);
                    }
                }
            }
        }
    }
}

