namespace PBall.Controllers;

public partial class LeagueController : ControllerBase, ILeagueController
{
    private async Task<bool> CheckLoggedIn()
    {
        if (RouteData.Values["culture"] != null)
        {
            PBallRes.Culture = new CultureInfo($"{ RouteData.Values["culture"] }");

            if (Configuration != null)
            {
                string token = Request.Headers["Authentication"].ToString();

                token = token.Replace("Bearer ", "");

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(token);

                if (jwtSecurityToken != null)
                {
                    if (jwtSecurityToken.Header["alg"].ToString() == "HS256" && jwtSecurityToken.Header["typ"].ToString() == "JWT")
                    {
                        if (LoggedInService != null)
                        {
                            string? LoginEmail = jwtSecurityToken.Payload["name"].ToString();

                            Contact? contact = (from c in LoggedInService.LoggedInContactList
                                                where c.LoginEmail == LoginEmail
                                                select c).FirstOrDefault();

                            if (contact != null)
                            {
                                return await Task.FromResult(true);
                            }
                        }
                    }
                }
            }
        }

        return await Task.FromResult(false);
    }
}

