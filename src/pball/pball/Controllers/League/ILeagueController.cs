namespace PBall.Controllers;

public partial interface ILeagueController
{
    Task<ActionResult<League>> AddLeagueAsync(League league);
    Task<ActionResult<League>> DeleteLeagueAsync(int LeagueID);
    Task<ActionResult<List<League>>> GetPlayerLeaguesAsync();
    Task<ActionResult<League>> ModifyLeagueAsync(League league);
}
