using System.Globalization;

namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [Route("GetContactLeagueGamesBetweenDates")]
    [HttpPost]
    public async Task<ActionResult<List<Game>>> GetContactLeagueGamesBetweenDatesAsync(ContactLeagueGamesModel contactLeagueGamesModel)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (GameService != null)
        {
            return await GameService.GetContactLeagueGamesBetweenDatesAsync(contactLeagueGamesModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

