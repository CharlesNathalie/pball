namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_Good_Test(string culture)
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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        LoginEmailModel loginEmailModel = new LoginEmailModel()
                        {
                            LoginEmail = contact.LoginEmail,
                        };

                        var actionRes = await ContactService.GenerateTempCodeAsync(loginEmailModel);
                        bool? boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                        contact = (from c in db.Contacts
                                   orderby c.ContactID
                                   select c).FirstOrDefault();

                        Assert.NotNull(contact);
                        if (contact != null)
                        {
                            Assert.NotEmpty(contact.ResetPasswordTempCode);

                            contact.ResetPasswordTempCode = "";
                            try
                            {
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
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GenerateTempCodeAsync_LognEmail_Error_Test(string culture)
    {
        Assert.True(await _ContactServiceSetupAsync(culture));

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
                                        select c).FirstOrDefault();

                    Assert.NotNull(contact);
                    if (contact != null)
                    {
                        LoginEmailModel loginEmailModel = new LoginEmailModel()
                        {
                            LoginEmail = contact.LoginEmail,
                        };

                        loginEmailModel.LoginEmail = "ThisWillNotBeFound";

                        var actionRes = await ContactService.GenerateTempCodeAsync(loginEmailModel);
                        bool? boolRet = await DoBadRequestBoolTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "LoginEmail", loginEmailModel.LoginEmail.ToString()), actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}

