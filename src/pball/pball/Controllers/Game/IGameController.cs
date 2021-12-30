namespace PBall.Controllers;

public partial interface IGameController
{
    Task<ActionResult<Game>> AddGameAsync(Game game);
    Task<ActionResult<Game>> DeleteGameAsync(int GameID);
    Task<ActionResult<List<Game>>> GetAllLeagueGamesBetweenDatesAsync(LeagueGamesModel leagueGamesModel);
    Task<ActionResult<Game>> ModifyGameAsync(Game game);
}
