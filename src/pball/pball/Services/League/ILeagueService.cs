namespace PBallServices;

public partial interface ILeagueService
{
    Task<ActionResult<League>> AddLeagueAsync(League league);
    Task<ActionResult<League>> DeleteLeagueAsync(int LeagueID);
    Task<ActionResult<League>> ModifyLeagueAsync(League league);
}

