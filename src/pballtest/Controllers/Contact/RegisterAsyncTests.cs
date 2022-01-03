namespace pball.Controllers.Tests;

public partial class ContactControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_Good_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        RegisterModel registerModel = await FillRegisterModelAsync();

        Contact? contact = await DoOkRegister(registerModel, culture);
        Assert.NotNull(contact);
        if (contact != null)
        {
            Assert.True(contact.ContactID > 0);
        }

        if (db != null)
        {
            List<Contact> contactList = (from c in db.Contacts
                                         select c).ToList();

            Assert.NotNull(contactList);
            Assert.True(contactList.Any());
            Assert.Single(contactList);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task RegisterAsync_Error_Test(string culture)
    {
        Assert.True(await ContactControllerSetup(culture));

        RegisterModel registerModel = await FillRegisterModelAsync();

        registerModel.LoginEmail = "";

        ErrRes? errRes = await DoBadRequestRegister(registerModel, culture);
        Assert.NotNull(errRes);
        if (errRes != null)
        {
            Assert.NotEmpty(errRes.ErrList);
        }
    }
}

