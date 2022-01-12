namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult<Contact>> RegisterAsync(RegisterModel registerModel)
    {
        ErrRes errRes = new ErrRes();

        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData))
            {
                errRes.ErrList.Add(string.Format(PBallRes.LanguageNotSelected));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        if (ContactService != null)
        {
            return await ContactService.RegisterAsync(registerModel);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "ContactService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

