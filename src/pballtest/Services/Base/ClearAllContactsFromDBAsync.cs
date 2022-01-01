namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<bool> ClearAllContactsFromDBAsync()
    {
        if (db != null)
        {
            List<Contact> contactList = (from c in db.Contacts
                                         select c).ToList();

            try
            {
                db.Contacts?.RemoveRange(contactList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactList = new List<Contact>();
        }

        return await Task.FromResult(true);
    }
}

