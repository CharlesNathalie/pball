using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class ContactController : ControllerBase, IContactController
{
    private IConfiguration Configuration { get; }
    private IContactService ContactService { get; }
    private PBallContext db { get; }

    public ContactController(IConfiguration Configuration, IContactService ContactService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.ContactService = ContactService;
        this.db = db; 
    }
}

