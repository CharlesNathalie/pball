using System.Globalization;

namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [HttpPut]
    public async Task<ActionResult<Game>> ModifyGameAsync(Game game)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (GameService != null)
        {
            return await GameService.ModifyGameAsync(game);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

