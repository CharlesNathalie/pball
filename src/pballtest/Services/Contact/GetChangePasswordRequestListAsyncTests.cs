namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetChangePasswordRequestListAsync_Good_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;

                            var actionRes = await ContactService.GetChangePasswordRequestListAsync();
                            List<ChangePasswordModel>? changePasswordModelList = await DoOKTestReturnChangePasswordModelListAsync(actionRes);
                            Assert.NotNull(changePasswordModelList);
                            Assert.NotEmpty(changePasswordModelList);
                        }
                    }
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetChangePasswordRequestListAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.Contacts);
            if (db.Contacts != null)
            {
                Contact? contact = (from c in db.Contacts
                                    select c).FirstOrDefault();

                Assert.NotNull(contact);
                if (contact != null)
                {
                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = null;

                            var actionRes = await ContactService.GetChangePasswordRequestListAsync();
                            bool? boolRet = await DoBadRequestChangePasswordModelListTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
                            Assert.NotNull(boolRet);
                            Assert.True(boolRet);
                        }
                    }
                }
            }
        }
    }
}

