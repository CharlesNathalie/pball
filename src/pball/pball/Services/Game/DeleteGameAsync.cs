namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    public async Task<ActionResult<Game>> DeleteGameAsync(int GameID)
    {
        ErrRes errRes = new ErrRes();

        if (LoggedInService.LoggedInContactInfo == null || LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (GameID == 0)
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "GameID"));
            return await Task.FromResult(BadRequest(errRes));
        }

        Game? game = (from c in db.Games
                      where c.GameID == GameID
                      select c).FirstOrDefault(); ;

        if (game == null)
        {
            errRes.ErrList.Add(string.Format(PBallRes.CouldNotFind_With_Equal_, "Game", "GameID", GameID.ToString()));
            return await Task.FromResult(BadRequest(errRes));
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
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        return await Task.FromResult(Ok(game));
    }
}

