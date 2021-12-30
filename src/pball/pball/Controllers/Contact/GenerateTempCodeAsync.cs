using System.Globalization;

namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("GenerateTempCode")]
    [HttpPost]
    public async Task<ActionResult<string>> GenerateTempCodeAsync(LeagueContactGenerateCodeModel lagueContactGenerateCodeModel)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (ContactService != null)
        {
            return await ContactService.GenerateTempCodeAsync(lagueContactGenerateCodeModel);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

