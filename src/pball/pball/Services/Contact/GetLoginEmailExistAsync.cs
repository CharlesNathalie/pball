namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<bool>> GetLoginEmailExistAsync(LoginEmailModel loginEmailModel)
    {
        if (string.IsNullOrWhiteSpace(loginEmailModel.LoginEmail))
        {
            return await Task.FromResult(Ok(false));
        }

        Contact? contact = (from c in db.Contacts
                            where c.LoginEmail == loginEmailModel.LoginEmail
                            select c).FirstOrDefault();

        if (contact != null)
        {
            return await Task.FromResult(Ok(true));
        }

        return await Task.FromResult(Ok(false));
    }
}

