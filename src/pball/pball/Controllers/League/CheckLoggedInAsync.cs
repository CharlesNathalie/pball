﻿namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    private async Task<bool> CheckLoggedIn()
    {
        if (RouteData.Values["culture"] != null)
        {
            PBallRes.Culture = new CultureInfo($"{ RouteData.Values["culture"] }");
            if (User != null)
            {
                if (User.Identity != null)
                {
                    if (LoggedInService != null)
                    {
                        if (await LoggedInService.SetLoggedInContactInfoAsync($"{ User.Identity.Name }"))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}

