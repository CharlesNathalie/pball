namespace PBall.Controllers;

public partial interface ILeagueContactController
{
    Task<ActionResult<LeagueContact>> AddLeagueContactAsync(LeagueContact leagueContact);
    Task<ActionResult<LeagueContact>> DeleteLeagueContactAsync(int LeagueContactID);
    Task<ActionResult<List<LeagueContact>>> GetLeagueContactsAsync(int LeagueID);
    Task<ActionResult<LeagueContact>> ModifyLeagueContactAsync(LeagueContact leagueContact);
}
