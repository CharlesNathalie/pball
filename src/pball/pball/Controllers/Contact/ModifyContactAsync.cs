namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [HttpPut]
    public async Task<ActionResult<Contact>> ModifyContactAsync(Contact contact)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (ContactService != null)
        {
            return await ContactService.ModifyContactAsync(contact);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

