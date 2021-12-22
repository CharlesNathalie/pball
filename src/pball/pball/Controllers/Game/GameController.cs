using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class GameController : ControllerBase, IGameController
{
    private IConfiguration Configuration { get; }
    private IGameService? GameService { get; }
    private ILoggedInService? LoggedInService { get; }
    private PBallContext db { get; }

    public GameController(IConfiguration Configuration, IGameService GameService, ILoggedInService LoggedInService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.GameService = GameService;
        this.LoggedInService = LoggedInService;
        this.db = db; 
    }
}

