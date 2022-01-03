namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("ClearServerLoggedInList")]
    [HttpGet]
    public async Task<ActionResult<bool>> ClearServerLoggedInListAsync()
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
        }

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactList = new List<Contact>();

            return await Task.FromResult(true);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LoggedInService")));
    }
}

