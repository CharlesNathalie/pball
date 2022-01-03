namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("GetFullNameExist")]
    [HttpPost]
    public async Task<ActionResult<bool>> GetFullNameExistAsync(FullNameModel fullNameModel)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
        }

        if (ContactService != null)
        {
            return await ContactService.GetFullNameExistAsync(fullNameModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

