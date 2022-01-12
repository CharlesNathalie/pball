namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<List<Game>>> GetAllLeagueGamesBetweenDatesAsync(LeagueGamesModel leagueGamesModel)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        League? league = (from c in db.Leagues
                          where c.LeagueID == leagueGamesModel.LeagueID
                          select c).FirstOrDefault();

        if (league == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", leagueGamesModel.LeagueID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueGamesModel.EndDate < leagueGamesModel.StartDate)
        {
            errRes.ErrList.Add(string.Format(PBallRes._DateIsBiggerThan_, "StartDate", "EndDate"));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok((from c in db.Games
                                         where c.LeagueID == leagueGamesModel.LeagueID
                                         && c.GameDate >= leagueGamesModel.StartDate
                                         && c.GameDate <= leagueGamesModel.EndDate
                                         select c).ToList()));
    }
}

