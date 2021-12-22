using System.Globalization;

namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("ChangePassword")]
    [HttpPost]
    public async Task<ActionResult<bool>> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (ContactService != null)
        {
            return await ContactService.ChangePasswordAsync(changePasswordModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

