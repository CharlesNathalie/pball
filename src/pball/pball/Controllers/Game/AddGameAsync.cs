using System.Globalization;

namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    //[Route("ChangePassword")]
    [HttpPost]
    public async Task<ActionResult<Game>> AddGameAsync(Game game)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (GameService != null)
        {
            return await GameService.AddGameAsync(game);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

