using System.Globalization;

namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("SendPasswordResetTempCodeAsync")]
    [HttpPost]
    public async Task<ActionResult<bool>> SendPasswordResetTempCodeAsync(LoginEmailModel loginEmailModel)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (ContactService != null)
        {
            return await ContactService.SendPasswordResetTempCodeAsync(loginEmailModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

