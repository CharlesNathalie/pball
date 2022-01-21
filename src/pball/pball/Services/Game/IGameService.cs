namespace PBallServices;

public partial interface IGameService
{
    Task<ActionResult<Game>> AddGameAsync(Game game);
    Task<ActionResult<Game>> DeleteGameAsync(int GameID);
    Task<ActionResult<List<Game>>> GetAllLeagueGamesAsync(int LeagueID);
    Task<ActionResult<List<Game>>> GetAllLeagueGamesBetweenDatesAsync(LeagueGamesModel leagueGamesModel);
    Task<ActionResult<Game>> ModifyGameAsync(Game game);
}

