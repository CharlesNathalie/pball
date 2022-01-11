namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<List<League>>> GetAllLeaguesAsync()
    {
        ErrRes errRes = new ErrRes();


        return await Task.FromResult(Ok((from c in db.Leagues
                                         select c).AsNoTracking().ToList()));
    }
}

