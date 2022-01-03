namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [Route("{GameID:int}")]
    [HttpDelete]
    public async Task<ActionResult<Game>> DeleteGameAsync(int GameID)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (GameService != null)
        {
            return await GameService.DeleteGameAsync(GameID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

