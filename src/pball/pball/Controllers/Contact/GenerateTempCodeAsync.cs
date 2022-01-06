namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("GenerateTempCode")]
    [HttpPost]
    public async Task<ActionResult<bool>> GenerateTempCodeAsync(LoginEmailModel loginEmailModel)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
        }

        if (ContactService != null)
        {
            return await ContactService.GenerateTempCodeAsync(loginEmailModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

