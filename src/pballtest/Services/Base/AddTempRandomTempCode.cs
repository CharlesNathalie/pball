namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<string> AddTempRandomTempCode()
    {
        Random random = new Random();

        string TempCode = $"{ random.Next(1000, 9999) }";

        if (db != null)
        {
            Contact? contactToAddTempCode = (from c in db.Contacts
                                             select c).FirstOrDefault();

            if (contactToAddTempCode != null)
            {
                contactToAddTempCode.ResetPasswordTempCode = TempCode;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        return await Task.FromResult(TempCode);
    }
}

