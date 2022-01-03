namespace PBallServices;

public partial class HelperService : ControllerBase, IHelperService
{
    public async Task<bool> SetCultureAsync(RouteData routeData)
    {
        if (routeData.Values["culture"] != null)
        {
            PBallRes.Culture = new CultureInfo($"{ routeData.Values["culture"] }");

            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }
}

