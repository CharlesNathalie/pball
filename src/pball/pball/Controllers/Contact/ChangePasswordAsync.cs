namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("ChangePassword")]
    [HttpPost]
    public async Task<ActionResult<bool>> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
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
            return await ContactService.ChangePasswordAsync(changePasswordModel);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "ContactService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

