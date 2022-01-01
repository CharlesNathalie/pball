using System.Globalization;

namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel)
    {
        if (ContactService != null)
        {
            return await ContactService.LoginAsync(loginModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

