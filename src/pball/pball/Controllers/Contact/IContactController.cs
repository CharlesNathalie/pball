namespace PBall.Controllers;

public partial interface IContactController
{
    Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel);
}
