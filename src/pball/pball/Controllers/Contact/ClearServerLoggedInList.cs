namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("ClearServerLoggedInList")]
    [HttpGet]
    public async Task<ActionResult<bool>> ClearServerLoggedInListAsync()
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

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactList = new List<Contact>();

            return await Task.FromResult(true);
        }

        errRes.ErrList.Add(string.Format(string.Format(PBallRes._IsRequired, "ContactService")));
        return await Task.FromResult(BadRequest(errRes));
    }
}

