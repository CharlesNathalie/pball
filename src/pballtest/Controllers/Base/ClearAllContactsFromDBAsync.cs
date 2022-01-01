namespace pball.Controllers.Tests;

public partial class BaseControllerTests
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

        return await Task.FromResult(true);
    }
}

