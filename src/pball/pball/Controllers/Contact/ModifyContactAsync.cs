using System.Globalization;

namespace PBall.Controllers;

public partial class ContactController : ControllerBase, IContactController
{
    [HttpPut]
    public async Task<ActionResult<Contact>> ModifyContactAsync(Contact contact)
    {
        if (!await CheckLoggedIn()) return await Task.FromResult(BadRequest(string.Format(PBallRes.YouDoNotHaveAuthorization)));

        if (ContactService != null)
        {
            return await ContactService.ModifyContactAsync(contact);
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "ContactService")));
    }
}

