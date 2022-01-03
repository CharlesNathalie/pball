namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("ChangePassword")]
    [HttpPost]
    public async Task<ActionResult<bool>> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
        }

        if (ContactService != null)
        {
            return await ContactService.ChangePasswordAsync(changePasswordModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

