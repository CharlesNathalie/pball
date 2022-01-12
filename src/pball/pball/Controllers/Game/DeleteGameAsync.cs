namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [Route("{GameID:int}")]
    [HttpDelete]
    public async Task<ActionResult<Game>> DeleteGameAsync(int GameID)
    {
        ErrRes errRes = new ErrRes();

        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData))
            {
                errRes.ErrList.Add(string.Format(PBallRes.LanguageNotSelected));
                return await Task.FromResult(BadRequest(errRes));
            }
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request))
            {
                errRes.ErrList.Add(string.Format(PBallRes.YouDoNotHaveAuthorization));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        if (GameService != null)
        {
            return await GameService.DeleteGameAsync(GameID);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "GameService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

