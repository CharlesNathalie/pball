namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    private IConfiguration Configuration { get; }
    private IUserService UserService { get; }
    private ILoggedInService LoggedInService { get;  }
    private IScrambleService ScrambleService { get; }
    private PBallContext db { get; }

    public ContactService(IConfiguration Configuration, IUserService UserService, IScrambleService ScrambleService, 
        ILoggedInService LoggedInService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.UserService = UserService;
        this.ScrambleService = ScrambleService;
        this.LoggedInService = LoggedInService;
        this.db = db;
    }
}

