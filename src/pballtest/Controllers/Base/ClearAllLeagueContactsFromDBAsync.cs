namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<bool> ClearAllLeagueContactsFromDBAsync()
    {
        if (db != null)
        {
            List<LeagueContact> leagueContactList = (from c in db.LeagueContacts
                                                     select c).ToList();

            try
            {
                db.LeagueContacts?.RemoveRange(leagueContactList);
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

