namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    private IConfiguration Configuration { get; }
    private ILoggedInService LoggedInService { get;  }
    private PBallContext db { get; }

    public GameService(IConfiguration Configuration, ILoggedInService LoggedInService, PBallContext db
       )
    {
        this.Configuration = Configuration;
        this.LoggedInService = LoggedInService;
        this.db = db;
    }
}

