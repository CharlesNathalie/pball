namespace PBallServices;

public partial interface IContactService
{
    Task<ActionResult<bool>> ChangePasswordAsync(PasswordChangeModel passwordChangeModel);
    Task<ActionResult<Contact>> DeleteContactAsync(int ContactID);
    Task<ActionResult<bool>> GetFullNameExistAsync(FullNameModel fullNameModel);
    Task<ActionResult<bool>> GetLoginEmailExistAsync(string loginEmail);
    Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel);
    Task<ActionResult<Contact>> ModifyContactAsync(Contact contact);
    Task<ActionResult<Contact>> RegisterAsync(RegisterModel registerModel);
    Task<ActionResult<bool>> SendPasswordResetTempCodeAsync(string LoginEmail);
}

