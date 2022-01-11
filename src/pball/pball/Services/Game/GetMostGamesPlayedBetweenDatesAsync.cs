namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<List<MostGamesPlayedModel>>> GetMostGamesPlayedBetweenDatesAsync(LeagueGamesModel leagueGamesModel)
    {
        ErrRes errRes = new ErrRes();

        if (leagueGamesModel.StartDate.Year < 2020)
        {
            leagueGamesModel.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        if (leagueGamesModel.EndDate.Year < 2020)
        {
            leagueGamesModel.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        if (leagueGamesModel.EndDate < leagueGamesModel.StartDate)
        {
            leagueGamesModel.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            leagueGamesModel.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        if (db != null)
        {
            if (db.Contacts != null && db.Games != null)
            {
                return await Task.FromResult(Ok((from c in db.Contacts
                                                 let gc = (from g in db.Games
                                                           where (c.ContactID == g.Team1Player1
                                                           || c.ContactID == g.Team1Player2
                                                           || c.ContactID == g.Team2Player1
                                                           || c.ContactID == g.Team2Player2)
                                                           && g.GameDate >= leagueGamesModel.StartDate
                                                           && g.GameDate <= leagueGamesModel.EndDate
                                                           && g.LeagueID == leagueGamesModel.LeagueID
                                                           select g).Count()
                                                 where gc > 0
                                                 orderby gc descending
                                                 select new MostGamesPlayedModel
                                                 {
                                                     ContactID = c.ContactID,
                                                     FullName = c.LastName + ", " + c.FirstName + (c.Initial.Length > 0 ? " " + c.Initial + "." : ""),
                                                     NumberOfGames = gc
                                                 }).AsNoTracking().ToList()));
            }
        }

        return await Task.FromResult(Ok(new List<MostGamesPlayedModel>()));
    }
}

