namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<Game>> ModifyGameAsync(Game game)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (game == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ShouldNotBeNullOrEmpty, "game")));
        }

        if (game.GameID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameID")));
        }

        Game? gameToChange = (from c in db.Games
                              where c.GameID == game.GameID
                              select c).FirstOrDefault();

        if (gameToChange == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.CouldNotFind_With_Equal_, "Game", "GameID", game.GameID.ToString())));
        }

        // Player1 -----------------------------------------------------------------------------------------------------
        if (game.Player1 == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player1")));
        }

        Contact? contact = (from c in db.Contacts
                            where c.ContactID == game.Player1
                            select c).FirstOrDefault(); ;

        if (contact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player1")));
        }

        // Player2 -----------------------------------------------------------------------------------------------------
        if (game.Player2 == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player2")));
        }

        contact = (from c in db.Contacts
                   where c.ContactID == game.Player2
                   select c).FirstOrDefault(); ;

        if (contact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player2")));
        }

        // Player3 -----------------------------------------------------------------------------------------------------
        if (game.Player3 == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player3")));
        }

        contact = (from c in db.Contacts
                   where c.ContactID == game.Player3
                   select c).FirstOrDefault(); ;

        if (contact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player3")));
        }

        // Player4 -----------------------------------------------------------------------------------------------------
        if (game.Player4 == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player4")));
        }

        contact = (from c in db.Contacts
                   where c.ContactID == game.Player4
                   select c).FirstOrDefault(); ;

        if (contact == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Player4")));
        }

        // Scores1 -----------------------------------------------------------------------------------------------------
        if (game.Scores1 < 0.0D)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MinValueIs_, "Scores1")));
        }

        // Scores2 -----------------------------------------------------------------------------------------------------
        if (game.Scores2 < 0.0D)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MinValueIs_, "Scores2")));
        }

        // Scores3 -----------------------------------------------------------------------------------------------------
        if (game.Scores3 < 0.0D)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MinValueIs_, "Scores3")));
        }

        // Scores4 -----------------------------------------------------------------------------------------------------
        if (game.Scores4 < 0.0D)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._MinValueIs_, "Scores4")));
        }

        gameToChange.Player1 = game.Player1;
        gameToChange.Player2 = game.Player2;
        gameToChange.Player3 = game.Player3;
        gameToChange.Player4 = game.Player4;
        gameToChange.Scores1 = game.Scores1;
        gameToChange.Scores2 = game.Scores2;
        gameToChange.Scores3 = game.Scores3;
        gameToChange.Scores4 = game.Scores4;
        gameToChange.GameDate = game.GameDate;
        gameToChange.LastUpdateDate_UTC = DateTime.UtcNow;
        gameToChange.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(Ok(gameToChange));
    }
}

