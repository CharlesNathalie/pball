using System.Globalization;

namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [Route("GetAllLeagueGamesBetweenDates")]
    [HttpPost]
    public async Task<ActionResult<List<Game>>> GetAllLeagueGamesBetweenDatesAsync(LeagueGamesModel leagueGamesModel)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (GameService != null)
        {
            return await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

