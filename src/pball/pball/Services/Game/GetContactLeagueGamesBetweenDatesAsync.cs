namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<List<Game>>> GetContactLeagueGamesBetweenDatesAsync(ContactLeagueGamesModel contactGroupGamesModel)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (contactGroupGamesModel.ContactID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactID")));
        }

        if (contactGroupGamesModel.LeagueID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GroupID")));
        }

        if (contactGroupGamesModel.StartDate.Year < 2020)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "StartDate")));
        }

        if (contactGroupGamesModel.EndDate.Year < 2020)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "EndDate")));
        }

        if (contactGroupGamesModel.EndDate.Year >= contactGroupGamesModel.StartDate.Year)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._DateIsBiggerThan_, "StartDate", "EndDate")));
        }

        return await Task.FromResult(Ok((from c in db.Games
                                         where c.LeagueID == contactGroupGamesModel.LeagueID
                                         && (c.Player1 == contactGroupGamesModel.ContactID
                                         || c.Player2 == contactGroupGamesModel.ContactID
                                         || c.Player3 == contactGroupGamesModel.ContactID
                                         || c.Player4 == contactGroupGamesModel.ContactID)
                                         && c.GameDate >= contactGroupGamesModel.StartDate
                                         && c.GameDate <= contactGroupGamesModel.EndDate
                                         select c).ToList()));
    }
}

