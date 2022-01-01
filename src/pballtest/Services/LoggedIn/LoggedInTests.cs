namespace pball.Services.Tests;

public partial class LoggedInServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginShouldAddOneContactInLoggedInContactList_Good_Test(string culture)
    {
        Assert.True(await _LoggedInServiceSetupAsync(culture));

        if (LoggedInService != null)
        {
            if (Configuration != null)
            {
                RegisterModel registerModel = await FillRegisterModelAsync();
                Contact? contact = await DoRegisterTestAsync(registerModel);
                Assert.NotNull(contact);

                    if (contact != null)
                    {
                        LoginModel loginModel = new LoginModel()
                        {
                            LoginEmail = registerModel.LoginEmail,
                            Password = registerModel.Password,
                        };

                    Contact? contact2 = await DoLoginTestAsync(loginModel);
                    Assert.NotNull(contact2);

                    if (contact2 != null)
                    {
                        Assert.True(contact2.ContactID > 0);
                        Assert.NotEmpty(LoggedInService.LoggedInContactList);
                        Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact2.ContactID).Any());
                    }
                }
            }
        }
    }
}

