namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Logoff/{ContactID:int}")]
    [HttpGet]
    public async Task<ActionResult<bool>> LogoffAsync(int ContactID)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (ContactService != null)
        {
            return await ContactService.LogoffAsync(ContactID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

