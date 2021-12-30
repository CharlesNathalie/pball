namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<List<Game>>> GetAllLeagueGamesBetweenDatesAsync(LeagueGamesModel leagueGamesModel)
    {
        ErrRes errRes = new ErrRes();

        if (LoggedInService.LoggedInContactInfo == null || LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueGamesModel.LeagueID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueGamesModel.StartDate.Year < 2020)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "StartDate"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (leagueGamesModel.EndDate.Year < 2020)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "EndDate"));
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

