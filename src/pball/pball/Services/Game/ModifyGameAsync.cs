namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<Game>> ModifyGameAsync(Game game)
    {
        ErrRes errRes = new ErrRes();

        if (LoggedInService.LoggedInContactInfo == null || LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (game.GameID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "GameID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Game? gameToChange = (from c in db.Games
                              where c.GameID == game.GameID
                              select c).FirstOrDefault(); ;

        if (gameToChange == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Game", "GameID", game.GameID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (game.LeagueID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueID", "0"));
            return await Task.FromResult(BadRequest(errRes));
        }

        League? league = (from c in db.Leagues
                          where c.LeagueID == game.LeagueID
                          select c).AsNoTracking().FirstOrDefault(); ;

        if (league == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LeagueID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // Team1Player1 -----------------------------------------------------------------------------------------------------
        if (game.Team1Player1 == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team1Player1"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Contact? contact = (from c in db.Contacts
                            where c.ContactID == game.Team1Player1
                            select c).AsNoTracking().FirstOrDefault(); ;

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team1Player1"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // Team1Player2 -----------------------------------------------------------------------------------------------------
        if (game.Team1Player2 == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team1Player2"));
            return await Task.FromResult(BadRequest(errRes));
        }

        contact = (from c in db.Contacts
                   where c.ContactID == game.Team1Player2
                   select c).AsNoTracking().FirstOrDefault(); ;

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team1Player2"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // Team2Player1 -----------------------------------------------------------------------------------------------------
        if (game.Team2Player1 == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team2Player1"));
            return await Task.FromResult(BadRequest(errRes));
        }

        contact = (from c in db.Contacts
                   where c.ContactID == game.Team2Player1
                   select c).AsNoTracking().FirstOrDefault(); ;

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team2Player1"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // Team2Player2 -----------------------------------------------------------------------------------------------------
        if (game.Team2Player2 == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team2Player2"));
            return await Task.FromResult(BadRequest(errRes));
        }

        contact = (from c in db.Contacts
                   where c.ContactID == game.Team2Player2
                   select c).AsNoTracking().FirstOrDefault(); ;

        if (contact == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Team2Player2"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // Team1Scores -----------------------------------------------------------------------------------------------------
        if (game.Team1Scores < 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MinValueIs_, "Team1Scores", "0"));
            return await Task.FromResult(BadRequest(errRes));
        }

        // Team2Scores -----------------------------------------------------------------------------------------------------
        if (game.Team2Scores < 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MinValueIs_, "Team2Scores", "0"));
            return await Task.FromResult(BadRequest(errRes));
        }

        gameToChange.LeagueID = game.LeagueID;
        gameToChange.Team1Player1 = game.Team1Player1;
        gameToChange.Team1Player2 = game.Team1Player2;
        gameToChange.Team2Player1 = game.Team2Player1;
        gameToChange.Team2Player2 = game.Team2Player2;
        gameToChange.Team1Scores = game.Team1Scores;
        gameToChange.Team2Scores = game.Team2Scores;
        gameToChange.GameDate = game.GameDate;
        gameToChange.Removed = false;
        gameToChange.LastUpdateDate_UTC = DateTime.UtcNow;
        gameToChange.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(gameToChange));
    }
}

