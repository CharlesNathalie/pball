namespace PBallServices;

public partial interface IGameService
{
    Task<ActionResult<Game>> AddGameAsync(Game game);
    Task<ActionResult<Game>> DeleteGameAsync(int GameID);
    Task<ActionResult<List<Game>>> GetContactLeagueGamesBetweenDatesAsync(ContactLeagueGamesModel contactLeagueGamesModel);
    Task<ActionResult<Game>> ModifyGameAsync(Game game);
}

