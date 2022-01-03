namespace PBallServices;

public partial class HelperService : ControllerBase, IHelperService
{
    public async Task<bool> CheckLoggedInAsync(RouteData routeData, HttpRequest request)
    {
        string token = request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(token))
        {
            return await Task.FromResult(false);
        }

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
                                        && c.Token == token
                                        select c).FirstOrDefault();

                    if (contact != null)
                    {
                        if (UserService != null)
                        {
                            UserService.User = contact;
                        }

                        return await Task.FromResult(true);
                    }
                }
            }
        }

        return await Task.FromResult(false);
    }
}

