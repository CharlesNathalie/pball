namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetLoginEmailExistAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        bool? boolRet = await ClearAllContactsFromDBAsync();
        Assert.True(boolRet);

        if (ContactService != null)
        {
            RegisterModel registerModel = await FillRegisterModelAsync();

            var actionAddRes = await ContactService.RegisterAsync(registerModel);
            Contact? contact = await DoOKTestReturnContactAsync(actionAddRes);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
            }

            LoginEmailModel loginEmailModel = new LoginEmailModel()
            {
                LoginEmail = registerModel.LoginEmail,
            };

            var actionRes = await ContactService.GetLoginEmailExistAsync(loginEmailModel);
            boolRet = await DoOKTestReturnBoolAsync(actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            loginEmailModel.LoginEmail = $"Not{ registerModel.LoginEmail}";

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

