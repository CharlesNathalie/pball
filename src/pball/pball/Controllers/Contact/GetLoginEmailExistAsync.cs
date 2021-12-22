﻿using System.Globalization;

namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("GetLoginEmailExist")]
    [HttpPost]
    public async Task<ActionResult<bool>> GetLoginEmailExistAsync(LoginEmailModel loginEmailModel)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (ContactService != null)
        {
            return await ContactService.GetLoginEmailExistAsync(loginEmailModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

