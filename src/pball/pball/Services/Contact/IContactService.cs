namespace PBallServices;

public partial interface IContactService
{
    Task<ActionResult<bool>> ChangePasswordAsync(ChangePasswordModel passwordChangeModel);
    Task<ActionResult<Contact>> DeleteContactAsync(int ContactID);
    Task<ActionResult<bool>> GenerateTempCodeAsync(LoginEmailModel loginEmailModel);
    Task<ActionResult<List<Player>>> GetAllPlayersForLeagueAsync(int LeagueID);
    Task<ActionResult<List<ChangePasswordModel>>> GetChangePasswordRequestListAsync();
    Task<ActionResult<bool>> GetFullNameExistAsync(FullNameModel fullNameModel);
    Task<ActionResult<bool>> GetLoginEmailExistAsync(LoginEmailModel loginEmailModel);
    Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel);
    Task<ActionResult<bool>> LogoffAsync(int ContactID);
    Task<ActionResult<Contact>> ModifyContactAsync(Contact contact);
    Task<ActionResult<Contact>> RegisterAsync(RegisterModel registerModel);
    Task<ActionResult<List<Player>>> SearchContactsAsync(int LeagueID, string SearchTerms);
}

