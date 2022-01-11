namespace pball.Services.Tests;

public partial class ContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetFullNameExistAsync_Good_Test(string culture)
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
                    FullNameModel fullNameModel = new FullNameModel()
                    {
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Initial = contact.Initial,
                    };

                    Assert.NotNull(ContactService);
                    if (ContactService != null)
                    {
                        var actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
                        bool? boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.True(boolRet);

                        fullNameModel.FirstName = $" { contact.FirstName }Not";

                        actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
                        boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.False(boolRet);

                        fullNameModel.LastName = $" { contact.LastName }Not";

                        actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
                        boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.False(boolRet);

                        fullNameModel.Initial = $" { contact.Initial }Not";

                        actionRes = await ContactService.GetFullNameExistAsync(fullNameModel);
                        boolRet = await DoOKTestReturnBoolAsync(actionRes);
                        Assert.NotNull(boolRet);
                        Assert.False(boolRet);
                    }
                }
            }
        }
    }
}

