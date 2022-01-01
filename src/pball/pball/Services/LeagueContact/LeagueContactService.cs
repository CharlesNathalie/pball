namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    private IUserService UserService { get; }
    private PBallContext db { get; }

    public LeagueContactService(IUserService UserService, PBallContext db)
    {
        this.UserService = UserService;
        this.db = db;
    }
}

