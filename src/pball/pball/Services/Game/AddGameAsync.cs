namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<Game>> AddGameAsync(Game game)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (game == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ShouldNotBeNullOrEmpty, "game")));
        }

        if (game.GameID != 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._ShouldBeEqualTo_, "GameID", "0")));
        }

        if (game.LeagueID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GroupID", "0")));
        }

        League? group = (from c in db.Leagues
                        where c.LeagueID == game.LeagueID
                        select c).FirstOrDefault(); ;

        if (group == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GroupID")));
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

        Game gameNew = new Game()
        {
            LeagueID = game.LeagueID,
            Player1 = game.Player1,
            Player2 = game.Player2,
            Player3 = game.Player3,
            Player4 = game.Player4,
            Scores1 = game.Scores1,
            Scores2 = game.Scores2,
            Scores3 = game.Scores3,
            Scores4 = game.Scores4,
            GameDate = game.GameDate,
            Removed = false,
            LastUpdateDate_UTC =  DateTime.UtcNow,
            LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID,
        };

        db.Games?.Add(gameNew);
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(Ok(gameNew));
    }
}

