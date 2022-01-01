namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    private IUserService UserService { get; }
    private PBallContext db { get; }

    public GameService(IUserService UserService, PBallContext db)
    {
        this.UserService = UserService;
        this.db = db;
    }
}

