namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("{ContactID:int}")]
    [HttpDelete]
    public async Task<ActionResult<Contact>> DeleteContactAsync(int ContactID)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (ContactService != null)
        {
            return await ContactService.DeleteContactAsync(ContactID);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

