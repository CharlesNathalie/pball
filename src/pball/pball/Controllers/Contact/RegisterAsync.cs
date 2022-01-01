using System.Globalization;

namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult<Contact>> RegisterAsync(RegisterModel registerModel)
    {
        if (ContactService != null)
        {
            return await ContactService.RegisterAsync(registerModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

