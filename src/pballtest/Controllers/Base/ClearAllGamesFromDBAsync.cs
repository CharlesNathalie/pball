namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<bool> ClearAllGamesFromDBAsync()
    {
        if (db != null)
        {
            List<Game> gameList = (from c in db.Games
                                   select c).ToList();

            try
            {
                db.Games?.RemoveRange(gameList);
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

