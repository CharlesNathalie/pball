namespace PBallServices;

public partial interface ILeagueService
{
    Task<ActionResult<League>> AddLeagueAsync(League league);
    Task<ActionResult<League>> DeleteLeagueAsync(int LeagueID);
    Task<ActionResult<List<League>>> GetLeagueListAsync(int Skip, int Take);
    Task<ActionResult<List<Player>>> GetLeagueAdminPlayerListAsync(int LeagueID);
    Task<ActionResult<List<League>>> GetPlayerLeaguesAsync();
    Task<ActionResult<League>> ModifyLeagueAsync(League league);
}

