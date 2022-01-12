namespace PBall.Controllers;

public partial class GameController : ControllerBase, IGameController
{
    [Route("GetAllLeagueGamesBetweenDates")]
    [HttpPost]
    public async Task<ActionResult<List<Game>>> GetAllLeagueGamesBetweenDatesAsync(LeagueGamesModel leagueGamesModel)
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
            return await GameService.GetAllLeagueGamesBetweenDatesAsync(leagueGamesModel);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "GameService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

