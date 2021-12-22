namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [Route("{GameID:int}")]
    [HttpDelete]
    public async Task<ActionResult<Game>> DeleteGameAsync(int GameID)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (GameService != null)
        {
            return await GameService.DeleteGameAsync(GameID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

