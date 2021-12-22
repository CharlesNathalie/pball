namespace PBallServices;

public partial class LeagueContactService : ControllerBase, ILeagueContactService
{
    private IConfiguration Configuration { get; }
    private ILoggedInService LoggedInService { get;  }
    private PBallContext db { get; }

    public LeagueContactService(IConfiguration Configuration, ILoggedInService LoggedInService, PBallContext db
       )
    {
        this.Configuration = Configuration;
        this.LoggedInService = LoggedInService;
        this.db = db;
    }
}

