namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteContactAsync_Good_Test(string culture)
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
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        var actionDeleteRes = await ContactService.DeleteContactAsync(contact.ContactID);
                        Contact? contact3 = await DoOKTestReturnContactAsync(actionDeleteRes);
                        
                        Assert.NotNull(contact3);
                        if (contact3 != null)
                        {
                            Assert.True(contact3.ContactID > 0);
                            Assert.True(contact3.Removed);
                        }
                    }

                    contact.Removed = false;
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
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteContactAsync_Authorization_Error_Test(string culture)
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
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = null;
                        }

                        var actionDeleteRes = await ContactService.DeleteContactAsync(contact.ContactID);
                        bool? boolRet = await DoBadRequestContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionDeleteRes);
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
    public async Task DeleteContactAsync_CouldNotFindContact_Error_Test(string culture)
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
                        Assert.NotNull(UserService);
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        int ContactID = -1;

                        var actionDeleteRes = await ContactService.DeleteContactAsync(ContactID);
                        bool? boolRet = await DoBadRequestContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", ContactID.ToString()), actionDeleteRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);
                    }
                }
            }
        }
    }
}

