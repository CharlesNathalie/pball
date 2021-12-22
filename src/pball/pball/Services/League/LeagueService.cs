namespace PBallServices;

public partial class LeagueService : ControllerBase, ILeagueService
{
    private IConfiguration Configuration { get; }
    private ILoggedInService LoggedInService { get;  }
    private PBallContext db { get; }

    public LeagueService(IConfiguration Configuration, ILoggedInService LoggedInService, PBallContext db
       )
    {
        this.Configuration = Configuration;
        this.LoggedInService = LoggedInService;
        this.db = db;
    }
}

