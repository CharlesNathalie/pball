namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    private IUserService UserService { get; }
    private PBallContext db { get; }

    public LeagueService(IUserService UserService, PBallContext db)
    {
        this.UserService = UserService;
        this.db = db;
    }
}

