//namespace pball.Controllers.Tests;

//public partial class BaseControllerTests
//{
//    protected async Task<bool> ClearAllLeaguesFromDBAsync()
//    {
//        if (db != null)
//        {
//            List<League> leagueList = (from c in db.Leagues
//                                       select c).ToList();

//            try
//            {
//                db.Leagues?.RemoveRange(leagueList);
//                db.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Assert.True(false, ex.Message);
//            }
//        }

//        return await Task.FromResult(true);
//    }
//}

