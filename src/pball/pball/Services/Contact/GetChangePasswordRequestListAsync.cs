namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<List<ChangePasswordModel>>> GetChangePasswordRequestListAsync()
    {
        ErrRes errRes = new ErrRes();

        if (UserService.User == null)
        {
            errRes.ErrList.Add(PBallRes.YouDoNotHaveAuthorization);
            return await Task.FromResult(BadRequest(errRes));
        }

        if (db != null && db.Contacts != null)
        {
            return await Task.FromResult(Ok((from c in db.Contacts
                                             where c.ResetPasswordTempCode != null 
                                             && c.ResetPasswordTempCode != ""
                                             select new ChangePasswordModel
                                             {
                                                 LoginEmail = c.LoginEmail,
                                                 FirstName = c.FirstName,
                                                 Password = "",
                                                 TempCode = c.ResetPasswordTempCode,
                                             }).AsNoTracking().ToList()));
        }

        return await Task.FromResult(Ok((new List<Contact>())));
    }

}


