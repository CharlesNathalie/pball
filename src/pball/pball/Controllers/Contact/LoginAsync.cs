namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel)
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
            return await ContactService.LoginAsync(loginModel);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "ContactService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

