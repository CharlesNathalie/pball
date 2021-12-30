namespace PBallServices;

public partial interface IContactService
{
    Task<ActionResult<bool>> ChangePasswordAsync(ChangePasswordModel passwordChangeModel);
    Task<ActionResult<Contact>> DeleteContactAsync(int ContactID);
    Task<ActionResult<string>> GenerateTempCodeAsync(LeagueContactGenerateCodeModel leagueContactGenerateCodeModel);
    Task<ActionResult<bool>> GetFullNameExistAsync(FullNameModel fullNameModel);
    Task<ActionResult<bool>> GetLoginEmailExistAsync(LoginEmailModel loginEmailModel);
    Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel);
    Task<ActionResult<Contact>> ModifyContactAsync(Contact contact);
    Task<ActionResult<Contact>> RegisterAsync(RegisterModel? registerModel);
}

