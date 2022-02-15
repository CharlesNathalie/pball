namespace PBallServices;

public partial class HelperService : ControllerBase, IHelperService
{
    private IUserService? UserService { get; }
    private ILoggedInService? LoggedInService { get; }
    private PBallContext db { get; }

    public HelperService(IUserService UserService, ILoggedInService LoggedInService, PBallContext db)
    {
        this.UserService = UserService;
        this.LoggedInService = LoggedInService;
        this.db = db;
    }
}

