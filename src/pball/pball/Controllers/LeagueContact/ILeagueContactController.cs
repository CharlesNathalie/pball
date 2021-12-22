namespace PBall.Controllers;

public partial interface ILeagueContactController
{
    Task<ActionResult<LeagueContact>> AddLeagueContactAsync(LeagueContact leagueContact);
    Task<ActionResult<bool>> DeleteLeagueContactAsync(int LeagueContactID);
    //Task<ActionResult<LeagueContact>> ModifyLeagueContactAsync(LeagueContact leagueContact);
}
