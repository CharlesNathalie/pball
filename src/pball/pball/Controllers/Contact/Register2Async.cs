namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [Route("Register2")]
    [HttpPost]
    public async Task<ActionResult<RegisterModel>> Register2Async(RegisterModel registerModel)
    {
        return await Task.FromResult(Ok(registerModel));
    }
}

