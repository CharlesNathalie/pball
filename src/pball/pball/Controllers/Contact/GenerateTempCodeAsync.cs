namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("GenerateTempCode")]
    [HttpPost]
    public async Task<ActionResult<string>> GenerateTempCodeAsync(LeagueContactGenerateCodeModel lagueContactGenerateCodeModel)
    {
        if (HelperService != null)
        {
            if (!await HelperService.SetCultureAsync(RouteData)) return await Task.FromResult(BadRequest(string.Format(PBallRes.LanguageNotSelected)));
            if (!await HelperService.CheckLoggedInAsync(RouteData, Request)) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));
        }

        if (ContactService != null)
        {
            return await ContactService.GenerateTempCodeAsync(lagueContactGenerateCodeModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

