namespace PBallServices;

public partial class HelperService : ControllerBase, IHelperService
{
    private IUserService? UserService { get; }
    private ILoggedInService? LoggedInService { get; }

    public HelperService(IUserService UserService, ILoggedInService LoggedInService)
    {
        this.UserService = UserService;
        this.LoggedInService = LoggedInService;
    }
}

