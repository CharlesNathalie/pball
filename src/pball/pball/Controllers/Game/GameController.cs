using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class GameController : ControllerBase, IGameController
{
    private IConfiguration Configuration { get; }
     private IGameService? GameService { get; }
    private IUserService? UserService { get; }
    private ILoggedInService? LoggedInService { get; }
    private IHelperService? HelperService { get; }

    public GameController(IConfiguration Configuration, IUserService UserService, IGameService GameService, 
        ILoggedInService LoggedInService, IHelperService HelperService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.UserService = UserService;
        this.GameService = GameService;
        this.LoggedInService = LoggedInService;
        this.HelperService = HelperService;
    }
}

