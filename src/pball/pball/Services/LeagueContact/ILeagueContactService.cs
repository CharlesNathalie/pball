namespace PBallServices;

public partial interface ILeagueContactService
{
    Task<ActionResult<LeagueContact>> AddLeagueContactAsync(LeagueContact leagueContact);
    Task<ActionResult<LeagueContact>> DeleteLeagueContactAsync(int LeagueContactID);
    Task<ActionResult<LeagueContact>> ModifyLeagueContactAsync(LeagueContact leagueContact);
}

