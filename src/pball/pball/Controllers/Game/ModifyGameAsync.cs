using System.Globalization;

namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [HttpPut]
    public async Task<ActionResult<Game>> ModifyGameAsync(Game game)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (GameService != null)
        {
            return await GameService.ModifyGameAsync(game);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

