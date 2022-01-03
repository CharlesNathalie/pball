using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class LeagueContactController : ControllerBase, ILeagueContactController
{
    private IConfiguration Configuration { get; }
    private ILeagueContactService? LeagueContactService { get; }
    private ILoggedInService? LoggedInService { get; }
    private IHelperService? HelperService { get; }

    public LeagueContactController(IConfiguration Configuration, ILeagueContactService LeagueContactService, 
        ILoggedInService LoggedInService, IHelperService HelperService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.LeagueContactService = LeagueContactService;
        this.LoggedInService = LoggedInService;
        this.HelperService = HelperService;
    }
}

