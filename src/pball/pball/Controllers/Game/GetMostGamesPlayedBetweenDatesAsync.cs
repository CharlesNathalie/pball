using System.Globalization;

namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [Route("GetMostGamesPlayedBetweenDates")]
    [HttpPost]
    public async Task<ActionResult<List<MostGamesPlayedModel>>> GetMostGamesPlayedBetweenDatesAsync(LeagueGamesModel leagueGamesModel)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            //if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (GameService != null)
        {
            return await GameService.GetMostGamesPlayedBetweenDatesAsync(leagueGamesModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "GameService")));
    }
}

