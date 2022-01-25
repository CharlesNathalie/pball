namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    public async Task<ActionResult<List<League>>> GetLeagueListAsync(int Skip, int Take)
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (Take == 0)
        {
            Take = 1;
        }

        if (UserService.User != null)
        {

            if (db.Leagues != null)
            {
                return await Task.FromResult(Ok((from c in db.Leagues
                                                 orderby c.LeagueName ascending
                                                 select c).Skip(Skip).Take(Take).AsNoTracking().ToList()));
            }
        }

        return await Task.FromResult(Ok(new List<League>()));
    }
}

