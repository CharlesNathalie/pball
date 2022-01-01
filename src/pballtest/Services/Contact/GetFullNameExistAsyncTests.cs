namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetFullNameExistAsync_Good_Test(string culture)
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

            FullNameModel fullNameModel = new FullNameModel()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Initial = registerModel.Initial,
            };

            var actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
            boolRet = await DoOKTestReturnBoolAsync(actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            fullNameModel.FirstName = $" { registerModel.FirstName }Not";

            actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
            boolRet = await DoOKTestReturnBoolAsync(actionRes);
            Assert.NotNull(boolRet);
            Assert.False(boolRet);

            fullNameModel.LastName = $" { registerModel.LastName }Not";

            actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
            boolRet = await DoOKTestReturnBoolAsync(actionRes);
            Assert.NotNull(boolRet);
            Assert.False(boolRet);

            fullNameModel.Initial = $" { registerModel.Initial }Not";

            actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
            boolRet = await DoOKTestReturnBoolAsync(actionRes);
            Assert.NotNull(boolRet);
            Assert.False(boolRet);
        }
    }
}

