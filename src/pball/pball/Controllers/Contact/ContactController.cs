using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class ContactController : ControllerBase, IContactController
{
    private IConfiguration Configuration { get; }
    private IContactService? ContactService { get; }
    private IUserService? UserService { get; }
    private ILoggedInService? LoggedInService { get; }
    private PBallContext db { get; }

    public ContactController(IConfiguration Configuration, IUserService UserService, IContactService ContactService, ILoggedInService LoggedInService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.ContactService = ContactService;
        this.UserService = UserService;
        this.LoggedInService = LoggedInService;
        this.db = db; 
    }
}

