namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [HttpPost]
    public async Task<ActionResult<Game>> AddGameAsync(Game game)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (GameService != null)
        {
            return await GameService.AddGameAsync(game);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

