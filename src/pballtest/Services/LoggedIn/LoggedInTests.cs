namespace pball.Services.Tests;

public partial class LoggedInServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task LoginShouldAddOneContactInLoggedInContactList_Good_Test(string culture)
    {
        Assert.True(await _LoggedInServiceSetupAsync(culture));

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

            contact = await DoLoginTestAsync(loginModel);
            Assert.NotNull(contact);

            if (contact != null)
            {
                Assert.True(contact.ContactID > 0);
                if (LoggedInService != null)
                {
                    Assert.NotEmpty(LoggedInService.LoggedInContactList);
                    Assert.True(LoggedInService.LoggedInContactList.Where(c => c.ContactID == contact.ContactID).Any());
                }
            }

            Assert.NotNull(db);
            if (db != null)
            {
                Assert.NotNull(db.Contacts);
                if (db.Contacts != null)
                {
                    if (contact != null)
                    {
                        Contact? contactToDelete = (from c in db.Contacts
                                                    where c.ContactID == contact.ContactID
                                                    select c).FirstOrDefault();

                        Assert.NotNull(contactToDelete);
                        if (contactToDelete != null)
                        {
                            try
                            {
                                db.Contacts.Remove(contactToDelete);
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
}
