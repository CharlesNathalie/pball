﻿namespace PBall.Controllers;

public partial class LeagueContactController : ControllerBase, ILeagueContactController
{
    [Route("{LeagueContactID:int}")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteLeagueContactAsync(int LeagueContactID)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (LeagueContactService != null)
        {
            return await LeagueContactService.DeleteLeagueContactAsync(LeagueContactID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LeagueContactService")));
    }
}
