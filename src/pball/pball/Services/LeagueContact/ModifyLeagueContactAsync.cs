//namespace PBallServices;

//public partial class LeagueContactService : ControllerBase, ILeagueContactService
//{
//    public async Task<ActionResult<League>> ModifyLeagueContactAsync(LeagueContact leagueContact)
//    {
//        if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
//        {
//            return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
//        }

//        return await Task.FromResult(Ok(leagueContact));
//    }
//}

