namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<Game>> DeleteGameAsync(int GameID)
    {
        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        }

        if (GameID == 0)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameID")));
        }

        Game? game = (from c in db.Games
                      where c.GameID == GameID
                      select c).FirstOrDefault(); ;

        if (game == null)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.CouldNotFind_With_Equal_, "Game", "GameID", GameID.ToString())));
        }

        game.Removed = true;
        game.LastUpdateDate_UTC = DateTime.UtcNow;
        game.LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact == null ? 0 : LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID;

        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(Ok(game));
    }
}

