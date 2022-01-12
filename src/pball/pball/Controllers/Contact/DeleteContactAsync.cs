namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("{ContactID:int}")]
    [HttpDelete]
    public async Task<ActionResult<Contact>> DeleteContactAsync(int ContactID)
    {
        ErrRes errRes = new ErrRes();

        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData))
            {
                errRes.ErrList.Add(string.Format(PBallRes.LanguageNotSelected));
                return await Task.FromResult(BadRequest(errRes));
            }
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request))
            {
                errRes.ErrList.Add(string.Format(PBallRes.YouDoNotHaveAuthorization));
                return await Task.FromResult(BadRequest(errRes));
            }
        }

        if (ContactService != null)
        {
            return await ContactService.DeleteContactAsync(ContactID);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "ContactService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

