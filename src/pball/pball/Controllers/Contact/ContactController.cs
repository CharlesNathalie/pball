﻿using PBallServices;

namespace PBall.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public partial class ContactController : ControllerBase, IContactController
{
    private IConfiguration Configuration { get; }
    private IContactService? ContactService { get; }
    private ILoggedInService? LoggedInService { get; }
    private PBallContext db { get; }

    public ContactController(IConfiguration Configuration, IContactService ContactService, ILoggedInService LoggedInService, PBallContext db)
    {
        this.Configuration = Configuration;
        this.ContactService = ContactService;
        this.LoggedInService = LoggedInService;
        this.db = db; 
    }
}

