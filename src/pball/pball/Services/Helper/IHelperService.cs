namespace PBallServices;

public partial interface IHelperService
{
    Task<bool> CheckLoggedInAsync(RouteData routeData, HttpRequest request);
    Task<bool> SetCultureAsync(RouteData routeData);
}
