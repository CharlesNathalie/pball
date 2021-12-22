namespace PBallServices;

public partial interface ILeagueContactService
{
    Task<ActionResult<LeagueContact>> AddLeagueContactAsync(LeagueContact leagueContact);
    Task<ActionResult<bool>> DeleteLeagueContactAsync(int LeagueContactID);
    //Task<ActionResult<LeagueContact>> ModifyLeagueContactAsync(LeagueContact leagueContact);
}

