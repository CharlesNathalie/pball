namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel)
    {
        return await ContactService.LoginAsync(loginModel);
    }
}

