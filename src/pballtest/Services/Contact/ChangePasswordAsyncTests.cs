namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        if (ContactService != null)
        {
            string TempCode = await AddTempRandomTempCode();

            if (Configuration != null)
            {
                ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = $"{ Configuration["Password"] }New",
                    TempCode = TempCode,
                };
                var actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                bool? boolRet = await DoOKTestReturnBoolAsync(actionRes);
                Assert.True(boolRet);

                LoginModel loginModel = new LoginModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = $"{ Configuration["Password"] }New",
                };

                var actionLoginRes = await ContactService.LoginAsync(loginModel);
                Contact? contact = await DoOKTestReturnContactAsync(actionLoginRes);
                if (contact != null)
                {
                    Assert.Empty(contact.PasswordHash);
                    Assert.NotEmpty(contact.Token);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ChangePasswordAsync_LoginEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        if (ContactService != null)
        {
            string TempCode = await AddTempRandomTempCode();

            if (Configuration != null)
            {
                ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                {
                    LoginEmail = "",
                    Password = $"{ Configuration["Password"] }New",
                    TempCode = TempCode,
                };
                var actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                bool? boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "LoginEmail"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                changePasswordModel.LoginEmail = "NotExist" + Configuration["LoginEmail"];

                actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "LoginEmail", changePasswordModel.LoginEmail), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
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
            string TempCode = await AddTempRandomTempCode();

            if (Configuration != null)
            {
                ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                {
                    LoginEmail = Configuration["LoginEmail"],
                    Password = "",
                    TempCode = TempCode,
                };
                var actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                bool? boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._IsRequired, "Password"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                changePasswordModel.Password = "a".PadRight(51);

                actionRes = await ContactService.ChangePasswordAsync(changePasswordModel);
                boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes._MaxLengthIs_, "Password", "50"), actionRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

