namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult<Contact>> RegisterAsync(RegisterModel registerModel)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
        }

        if (ContactService != null)
        {
            return await ContactService.RegisterAsync(registerModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

