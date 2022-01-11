namespace PBallServices;

public partial interface ILeagueContactService
{
    Task<ActionResult<LeagueContact>> AddLeagueContactAsync(LeagueContact leagueContact);
    Task<ActionResult<LeagueContact>> DeleteLeagueContactAsync(int LeagueContactID);
    Task<ActionResult<List<LeagueContact>>> GetLeagueContactsAsync(int LeagueID);
    Task<ActionResult<LeagueContact>> ModifyLeagueContactAsync(LeagueContact leagueContact);
}

