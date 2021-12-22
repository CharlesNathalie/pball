using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class LeagueContactController : ControllerBase, ILeagueContactController
{
    private IConfiguration Configuration { get; }
    private ILeagueContactService? LeagueContactService { get; }
    private ILoggedInService? LoggedInService { get; }
    private PBallContext db { get; }

    public LeagueContactController(IConfiguration Configuration, ILeagueContactService LeagueContactService, ILoggedInService LoggedInService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.LeagueContactService = LeagueContactService;
        this.LoggedInService = LoggedInService;
        this.db = db; 
    }
}

