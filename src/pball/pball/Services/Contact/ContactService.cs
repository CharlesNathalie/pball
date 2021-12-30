namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    private IConfiguration Configuration { get; }
    private ILoggedInService LoggedInService { get;  }
    private IScrambleService ScrambleService { get; }
    private PBallContext db { get; }

    public ContactService(IConfiguration Configuration, IScrambleService ScrambleService, 
        ILoggedInService LoggedInService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.ScrambleService = ScrambleService;
        this.LoggedInService = LoggedInService;
        this.db = db;
    }
}

