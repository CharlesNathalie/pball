namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<List<Game>>> GetAllLeagueGamesAsync(int LeagueID)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        League? league = (from c in db.Leagues
                          where c.LeagueID == LeagueID
                          select c).FirstOrDefault();

        if (league == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", LeagueID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok((from c in db.Games
                                         where c.LeagueID == LeagueID
                                         select c).ToList()));
    }
}

