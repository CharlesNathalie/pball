using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class LeagueController : ControllerBase, ILeagueController
{
    private IConfiguration Configuration { get; }
    private ILeagueService? LeagueService { get; }
    private ILoggedInService? LoggedInService { get; }
    private IHelperService? HelperService { get; }

    public LeagueController(IConfiguration Configuration, ILeagueService LeagueService, 
        ILoggedInService LoggedInService, IHelperService HelperService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.LeagueService = LeagueService;
        this.LoggedInService = LoggedInService;
        this.HelperService = HelperService;
    }
}

